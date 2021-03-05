using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task SendAsync(string SenderAddress, string RecipientAddress, string Subject, string Body, CancellationToken Cancel = default)
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

            await client.SendMailAsync(message, Cancel).ConfigureAwait(false);
        }

        public void Send(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
        {
            foreach (var recipient_address in RecipientsAddresses)
                Send(SenderAddress, recipient_address, Subject, Body);
        }

        public void SendParallel(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
        {
            foreach (var recipient_address in RecipientsAddresses)
                ThreadPool.QueueUserWorkItem(_ => Send(SenderAddress, recipient_address, Subject, Body));
        }

        public async Task SendAsync(string SenderAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body, CancellationToken Cancel = default)
        {
            foreach (var recipient_address in RecipientsAddresses)
            {
                Cancel.ThrowIfCancellationRequested();
                await SendAsync(SenderAddress, recipient_address, Subject, Body, Cancel)
                   .ConfigureAwait(false);
            }
        }

        public async Task SendParallelAsync(
            string SenderAddress, 
            IEnumerable<string> RecipientsAddresses, 
            string Subject, string Body,
            CancellationToken Cancel = default)
        {
            Cancel.ThrowIfCancellationRequested();

            var tasks = RecipientsAddresses.Select(recipient_address => 
                SendAsync(SenderAddress, recipient_address, Subject, Body, Cancel));

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
