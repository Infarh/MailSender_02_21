using System.Collections.Generic;
using MailSender.lib.Entities.Base;

namespace MailSender.lib.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        int Add(T item);

        void Update(T item);

        bool Remove(int id);
    }
}
