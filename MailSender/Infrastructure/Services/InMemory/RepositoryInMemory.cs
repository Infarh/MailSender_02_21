using System.Collections.Generic;
using System.Linq;
using MailSender.Models.Base;

namespace MailSender.Infrastructure.Services.InMemory
{
    public abstract class RepositoryInMemory<T> : IRepository<T> where T : Entity
    {
        private readonly List<T> _Items;
        private int _MaxId;

        protected RepositoryInMemory(IEnumerable<T> items)
        {
            _Items = items.ToList();
            _MaxId = _Items.Count > 0 ? _Items.Max(i => i.Id) + 1 : 1;
        }

        public IEnumerable<T> GetAll() => _Items;

        public T GetById(int id) => _Items.FirstOrDefault(s => s.Id == id);

        public int Add(T item)
        {
            if (_Items.Contains(item))
                return item.Id;

            item.Id = _MaxId++;
            _Items.Add(item);
            return item.Id;
        }

        public abstract void Update(T item);

        public bool Remove(int id) => _Items.RemoveAll(s => s.Id == id) > 0;
    }
}
