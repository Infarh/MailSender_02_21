using System;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace TestWPF
{
    public class EmailSenderServiceClass
    {
        private string _userName;
        private SecureString _password;
        private SmtpClient _client;
        public EmailSenderServiceClass(string userName, SecureString password)
        {
            _userName = userName;
            _password = password;
        }
        public void SendMessage(MailMessage message)
        {
            InitClient();
            _client.Send(message);
        }

        private void InitClient()
        {
            var client = new SmtpClient(AppSettings.server, AppSettings.port);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = _userName,
                SecurePassword = _password
            };

            _client = client;
        }
    }
}
