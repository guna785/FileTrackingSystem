using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Helper
{
    public class EmailHelper
    {
        public async Task<bool> SendEmail(string userEmail, string confirmationLink)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path);
            var root = config.Build();
            var email = root.GetSection("EmailSettings:email").Value;
            var password = root.GetSection("EmailSettings:password").Value;
            var server = root.GetSection("EmailSettings:server").Value;
            var port = root.GetSection("EmailSettings:port").Value;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(server);
            mail.From = new MailAddress(email);
            mail.To.Add(userEmail);

            mail.Subject = "Confirm your email";
            mail.IsBodyHtml = true;
            mail.Body = confirmationLink;

            SmtpServer.Port = Convert.ToInt32(port);

            SmtpServer.Credentials = new System.Net.NetworkCredential(email, password);
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object es, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                await SmtpServer.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
