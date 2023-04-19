using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MBankWebScraping.Models;
using MBankWebScraping.Engine;

namespace MBankWebScraping.Engine
{
    internal class CurrencyScrapData
    {
        public IEnumerable<CurrencyModel> GetCurrency(string baseUrl)
        {
            var BaseUrl = baseUrl;

            var web = new HtmlWeb();
            var document = web.Load(BaseUrl);
            var tableRows = document.QuerySelectorAll("table tr").Skip(1);

            foreach (var row in tableRows)
            {
                var tds = row.QuerySelectorAll("td");

                if (tds.Count == 9 && !tds[0].InnerText.StartsWith("\n"))
                {
                    var currency = tds[0].InnerText;
                    var currencyShrotcut = tds[1].InnerText;
                    var country = tds[2].InnerText;
                    var reference_number = tds[3].InnerText;
                    var purchase_price = float.Parse(tds[4].InnerText, CultureInfo.InvariantCulture.NumberFormat);
                    var selling_price = float.Parse(tds[5].InnerText, CultureInfo.InvariantCulture.NumberFormat);
                    var average_price = float.Parse(tds[6].InnerText, CultureInfo.InvariantCulture.NumberFormat);

                    yield return new CurrencyModel(currencyShrotcut, currency, country, reference_number, purchase_price, selling_price, average_price);
                }
            }
        }
    }
}
