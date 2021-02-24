using System.Collections.Generic;
using System.Linq;

using MailSender.lib.Service;
using MailSender.Models;

namespace MailSender.Infrastructure.Services.InMemory
{
    class ServersRepository : IRepository<Server>
    {
        private List<Server> _Servers;
        private int _MaxId;

        public ServersRepository()
        {
            _Servers = Enumerable.Range(1, 10)
               .Select(i => new Server
               {
                   Id = i,
                   Name = $"Сервер-{i}",
                   Address = $"smtp.server{i}.com",
                   Login = $"Login-{i}",
                   Password = TextEncoder.Encode($"Password-{i}", 7),
                   UseSSL = i % 2 == 0,
               })
               .ToList();
            _MaxId = _Servers.Max(s => s.Id);
        }

        public IEnumerable<Server> GetAll() => _Servers;
        public Server GetById(int id) => _Servers.FirstOrDefault(s => s.Id == id);

        public int Add(Server item)
        {
            if (_Servers.Contains(item))
                return item.Id;
            item.Id = ++_MaxId;
            _Servers.Add(item);
            return item.Id;
        }

        public void Update(Server item)
        {
            if(_Servers.Contains(item))
                return;

            var db_item = GetById(item.Id);
            if (db_item is null) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Port = item.Port;
            db_item.Login = item.Login;
            db_item.Password = item.Password;
        }

        public bool Remove(int id) => _Servers.RemoveAll(s => s.Id == id) > 0;
    }
}
