using System.Net;
using System.Net.Mail;
using MailSender.lib.Interfaces;

namespace MailSender.lib
{
    public class SmtpMailService : IMailService
    {
        public IMailSender GetSender(string Server, int Port, bool SSL, string Login, string Password) => 
            new SmtpMailSender(Server, Port, SSL, Login, Password);
    }

    internal class SmtpMailSender : IMailSender
    {
        private readonly string _Server;
        private readonly int _Port;
        private readonly bool _SSL;
        private readonly string _Login;
        private readonly string _Password;

        public SmtpMailSender(string Server, int Port, bool SSL, string Login, string Password)
        {
            _Server = Server;
            _Port = Port;
            _SSL = SSL;
            _Login = Login;
            _Password = Password;
        }

        public void Send(string SenderAddress, string RecipientAddress, string Subject, string Body)
        {
            var from = new MailAddress(SenderAddress);
            var to = new MailAddress(RecipientAddress);

            using var message = new MailMessage(from, to)
            {
                Subject = Subject,
                Body = Body
            };

            using var client = new SmtpClient(_Server, _Port)
            {
                EnableSsl = _SSL,
                Credentials = new NetworkCredential
                {
                    UserName = _Login,
                    Password = _Password
                }
            };

            client.Send(message);
        }
    }
}
