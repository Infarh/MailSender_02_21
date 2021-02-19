using System.Collections.Generic;
using System.Linq;
using MailSender.lib.Service;
using MailSender.Models;

namespace MailSender.Infrastructure
{
    class ServersRepository
    {
        private List<Server> _Servers;

        public ServersRepository()
        {
            _Servers = Enumerable.Range(1, 10)
               .Select(i => new Server
                {
                    Name = $"Сервер-{i}",
                    Address = $"smtp.server{i}.com",
                    Login = $"Login-{i}",
                    Password = TextEncoder.Encode($"Password-{i}", 7),
                    UseSSL = i % 2 == 0,
                })
               .ToList();
        }

        public IEnumerable<Server> GetAll() => _Servers;

        public void Add(Server server)
        {
            _Servers.Add(server);
        }

        public void Remove(Server server)
        {
            _Servers.Remove(server);
        }
    }
}
