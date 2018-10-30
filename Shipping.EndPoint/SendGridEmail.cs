using NServiceBus.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Shipping.EndPoint
{
    class SendGridEmail
    {
        static ILog log = LogManager.GetLogger<SendGridEmail>();

        public static void Submit(string sender, string receiver,string subject)
        {
            Execute(sender, receiver, subject).Wait();
        }

        static async Task Execute(string sender, string receiver, string mailSubject)
        {
            var apiKey = Environment.GetEnvironmentVariable("Morita-SendGridAPIKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sender, sender);
            var subject = mailSubject;
            var to = new EmailAddress(receiver, receiver);
            var plainTextContent = "SendGrid is awesome";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            log.Info("Email submitted");
        }
    }
}
