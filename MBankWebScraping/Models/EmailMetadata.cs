using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBankWebScraping.Models
{
    internal class EmailMetadata
    {
        public static string? fromAddressMailAdress { get; set; }
        public static string? fromAddressName { get; set; }
        public static string? toAdressMailAdress { get; set; }
        public static string? toAddressName { get; set; }
        public static string? subject { get; set; }
        public static string? mailPassword { get; set; }
    }
}
