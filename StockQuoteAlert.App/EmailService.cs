using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
namespace StockQuoteAlert.App {
    public class EmailService {
        private readonly SmtpClient _smtpClient;
        public EmailService(string host, int port, string emailAddress, string emailPassword) {
            _smtpClient = new SmtpClient() {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailAddress, emailPassword),
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