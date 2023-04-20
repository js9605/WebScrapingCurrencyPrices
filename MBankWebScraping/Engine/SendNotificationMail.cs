using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using MBankWebScraping.Models;

namespace MBankWebScraping.Engine
{
    internal class SendNotificationMail
    {
        public SendNotificationMail(float sellingThreshold, float purchasePrice, string currencyName, string mailBody)
        {
            LoadCredentials();
            SendMail(sellingThreshold, purchasePrice, currencyName, mailBody);
            Console.WriteLine("INFORMATION: Mail send");
        }

        public void SendMail(float sellingThreshold, float purchasePrice, string currencyName, string mailBody)
        {
            var fromAddress = new MailAddress(EmailMetadata.fromAddressMailAdress, EmailMetadata.fromAddressName);
            var toAddress = new MailAddress(EmailMetadata.toAdressMailAdress, EmailMetadata.toAddressName);
            string subject = EmailMetadata.subject;
            string body = mailBody;

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com", // or any other SMTP server
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, EmailMetadata.mailPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }

        public void LoadCredentials()
        {
            StreamReader reader = new StreamReader("C:\\MBankWebScraping\\WebScrapingCurrencyPrices\\MBankWebScraping\\MailCredentials.txt");
            string[] data = reader.ReadToEnd().Split(",");

            EmailMetadata.fromAddressMailAdress = data[0];
            EmailMetadata.fromAddressName = data[1];
            EmailMetadata.toAdressMailAdress = data[2];
            EmailMetadata.toAddressName = data[3];
            EmailMetadata.subject = data[4];
            EmailMetadata.mailPassword = data[5];
        }
    }
}
