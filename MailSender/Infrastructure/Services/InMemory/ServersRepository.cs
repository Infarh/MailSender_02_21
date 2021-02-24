using System.Linq;

using MailSender.lib.Service;
using MailSender.Models;

namespace MailSender.Infrastructure.Services.InMemory
{
    class ServersRepository : RepositoryInMemory<Server>
    {
        public ServersRepository() : base(Enumerable.Range(1, 10)
           .Select(i => new Server
            {
                Id = i,
                Name = $"Сервер-{i}",
                Address = $"smtp.server{i}.com",
                Login = $"Login-{i}",
                Password = TextEncoder.Encode($"Password-{i}", 7),
                UseSSL = i % 2 == 0,
            }))
        {
        }

        public override void Update(Server item)
        {
            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Port = item.Port;
            db_item.Login = item.Login;
            db_item.Password = item.Password;
        }
    }
}
