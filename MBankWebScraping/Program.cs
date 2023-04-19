using CsvHelper;
using CsvHelper.Configuration.Attributes;
using MBankWebScraping.Models;
using MBankWebScraping.Engine;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


namespace MBankWebScraping
{
    class Program
    {
        static void Main()
        {
            ReadSettings();
            var url = ExternalSettings.SourceUrl;

            var currencyScraper = new CurrencyScrapData();
            var currencies = currencyScraper.GetCurrency(url); // insert GetCurrency into constructor like in CurrencyValueChecker

            //CHECK THRESHOLDS
            var valueChecker = new CurrencyValueChecker(currencies);

            //SAVE TO CSV CURRENCIES 
            SaveToCsv(currencies, ExternalSettings.FilePath);
            
        }

        static void SaveToCsv(IEnumerable<CurrencyModel> currencies, string csvPath)
        {
            int limiter = 0;

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvPath, true))
                {
                    file.WriteLine($"{DateTime.Now.AddDays(-1)}--------------------------------------------------------");
                    foreach (var currency in currencies)
                    {
                        if(limiter < 18)
                        {
                            file.WriteLine(($"{currency.country} {currency.currency}, PURCHASE PRICE: {currency.purchasePrice}, SELLING PRICE: {currency.sellingPrice}").ToString());
                            limiter += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("OOpsie. Im not working xD");
            }
        }

        static void ReadSettings()
        {
            StreamReader reader = new StreamReader("C:\\Finanse\\currencies_scraper\\InternalSettings.txt");

            string[] data = reader.ReadToEnd().Split(",");

            ExternalSettings.SourceUrl = data[0];
            ExternalSettings.FilePath = data[1];            
        }
    }
}
