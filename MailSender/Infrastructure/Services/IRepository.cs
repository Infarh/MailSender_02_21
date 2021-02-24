using System.Collections.Generic;
using MailSender.Models.Base;

namespace MailSender.Infrastructure.Services
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
