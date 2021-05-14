using System;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
namespace StockQuoteAlert{
    public class EmailService{
        private readonly SmtpClient _smtpClient;
        public EmailService(){
            _smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("brenodjangoemail@gmail.com", "Breno1999"),
            };
        }
        public async Task SendMail(string toEmailAddress, string emailSubject, string emailMessage){
            try{
                await _smtpClient.SendMailAsync("brenodjangoemail@gmail.com", toEmailAddress, emailSubject, emailMessage);
            }
            catch(Exception ex){
                Console.WriteLine($"Erro ao enviar email: {ex.ToString()}");
            }
        }
    }
}