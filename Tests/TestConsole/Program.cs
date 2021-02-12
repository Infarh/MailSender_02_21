using System;
using System.Net;
using System.Net.Mail;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var from = new MailAddress("shmachilin@yandex.ru", "Павел");
            var to = new MailAddress("shmachilin@gmail.com", "Павел");

            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            var client = new SmtpClient("smtp.yandex.ru", 465);
            client.EnableSsl = true;
            client.Timeout = 1000;

            client.Credentials = new NetworkCredential
            {
                UserName = "UserName",
                //SecurePassword = 
                Password = "PAssword!"
            };

            client.Send(message);
        }
    }
}
