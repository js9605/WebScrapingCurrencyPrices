using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MBankWebScraping.Models
{
    public class CurrencyThresholdModel
    {
        public string Name { get; set; }
        public float SellingPrice { get; set; }
        public float PurchasePrice { get; set; }
    }
}
