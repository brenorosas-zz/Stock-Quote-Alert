using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
namespace StockQuoteAlert.App {
    public class EmailService {
        private readonly SmtpClient _smtpClient;
        public EmailService() {
            _smtpClient = new SmtpClient() {
                Host = Environment.GetEnvironmentVariable("SMTP_HOST"),
                Port = Convert.ToInt16(Environment.GetEnvironmentVariable("SMTP_PORT")),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("EMAIL_ADDRESS"), Environment.GetEnvironmentVariable("EMAIL_PASSWORD")),
            };
        }
        public async Task SendMail(string toEmailAddress, string emailSubject, string emailMessage) {
            try {
                await _smtpClient.SendMailAsync(Environment.GetEnvironmentVariable("EMAIL_ADDRESS"), toEmailAddress, emailSubject, emailMessage);
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao enviar email: {ex.ToString()}");
            }
        }
    }
}