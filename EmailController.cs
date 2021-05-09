using System;
using System.Net;
using System.Net.Mail;
namespace StockQuoteAlert{
    public class EmailController{
        public SmtpClient client {get; set;}
        public EmailController(){
            this.client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("brenodjangoemail@gmail.com", "Breno1999"),
            };
        }
        public void SendMail(string toEmailAddress, string emailSubject, string emailMessage){
            try{
                this.client.Send("brenodjangoemail@gmail.com", toEmailAddress, emailSubject, emailMessage);
            }
            catch(Exception ex){
                Console.WriteLine($"Exception caught in CreateTestMessage(): {ex.ToString()}");
            }
        }
    }
}