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

                        //Console.WriteLine($"Currency data: {currencyModel.currencyShrotcut}    Current Purchase Price: {Math.Round(currencyModel.purchasePrice, 2)}zl; SELLING AT: {Math.Round(currencyThreshold.SellingPrice, 2)}zl");
                        //Console.WriteLine($"Currency data: {currencyModel.currencyShrotcut}    Current Selling Price: {Math.Round(currencyModel.sellingPrice, 2)}zl; PURCHASE AT: {Math.Round(currencyThreshold.PurchasePrice, 2)}zl");
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
                //Send Email / sms
                Console.WriteLine($"SELLING THRESHOLD EXCEEDED FOR |{currencyName}|");

                return true;
            }
            return false;
        }

        public bool CheckTresholdsPurchasePrice(float sellingThreshold, float purchasePrice, string currencyName)
        {
            if (sellingThreshold >= purchasePrice)
            {
                //Add information to Currencies log: currencies.csv e.g. "Purchase suggested"
                //Send Email / sms
                Console.WriteLine($"PURCHASE THRESHOLD EXCEEDED FOR |{currencyName}|");

                return true;
            }
            return false;
        }

    }
}