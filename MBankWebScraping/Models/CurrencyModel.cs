using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBankWebScraping.Models
{
    public class CurrencyModel
    {
        public CurrencyModel(string currencyShrotcut, string currency, string country, string reference_number, float purchase_price, float selling_price, float average_price)
        {
            this.currencyShrotcut = currencyShrotcut;
            this.currency = currency;
            this.country = country;
            referenceNumber = reference_number;
            purchasePrice = purchase_price;
            sellingPrice = selling_price;
            averagePrice = average_price;
        }
        public string currencyShrotcut { get; set; }
        public string currency { get; set; }
        public string country { get; set; }
        public string referenceNumber { get; set; }
        public float purchasePrice { get; set; }
        public float sellingPrice { get; set; }
        public float averagePrice { get; set; }

    }
}
