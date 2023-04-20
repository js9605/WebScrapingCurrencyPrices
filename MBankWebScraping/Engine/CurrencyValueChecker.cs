using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MBankWebScraping.Models;
using MBankWebScraping.Engine;
using Newtonsoft.Json;

namespace MBankWebScraping.Engine
{
    internal class CurrencyValueChecker
    {
        public IEnumerable<object> currenciesCollection;
        public string filePath = "C:\\Finanse\\currencies_scraper\\CurrenciesThresholds.json";

        public CurrencyValueChecker(IEnumerable<CurrencyModel> currencies)
        {

            var currencyThresholdModel = ReadThresholds(filePath);

            foreach (CurrencyThresholdModel currencyThreshold in currencyThresholdModel)
            {
                foreach(CurrencyModel currencyModel in currencies)
                {
                    if (currencyThreshold.Name == currencyModel.currencyShrotcut)
                    {
                        CheckTresholdsSellingPrice(currencyThreshold.SellingPrice, currencyModel.purchasePrice, currencyThreshold.Name);
                        CheckTresholdsPurchasePrice(currencyThreshold.PurchasePrice, currencyModel.sellingPrice, currencyThreshold.Name);
                    }
                }
            }  
        } 

        public CurrencyThresholdModel[] ReadThresholds(string filePath)
        {
            var json = File.ReadAllText(filePath);
            CurrencyThresholdModel[] currencyTresholdModels = JsonConvert.DeserializeObject<CurrencyThresholdModel[]>(json);

            return currencyTresholdModels;
        }

        public bool CheckTresholdsSellingPrice(float sellingThreshold, float purchasePrice, string currencyName)
        {
            if (sellingThreshold <= purchasePrice)
            {
                //Add information to Currencies log: currencies.csv e.g. "Selling suggested"
                var sendNotificationMail = new SendNotificationMail(sellingThreshold, purchasePrice, currencyName, $"For: {currencyName}: Threshold for selling exceeded! Threshold: {sellingThreshold}zl; Purchase Price: {purchasePrice}");
                Console.WriteLine($"SELLING THRESHOLD EXCEEDED FOR |{currencyName}|");

                return true;
            }
            return false;
        }

        public bool CheckTresholdsPurchasePrice(float purchaseThreshold, float sellingPrice, string currencyName)
        {
            if (purchaseThreshold >= sellingPrice)
            {
                //Add information to Currencies log: currencies.csv e.g. "Purchase suggested"
                var sendNotificationMail = new SendNotificationMail(purchaseThreshold, sellingPrice, currencyName, $"For: {currencyName}: Threshold for purchasing exceeded! Threshold: {purchaseThreshold}zl; Selling Price: {sellingPrice}");
                Console.WriteLine($"PURCHASE THRESHOLD EXCEEDED FOR |{currencyName}|");

                return true;
            }
            return false;
        }

    }
}