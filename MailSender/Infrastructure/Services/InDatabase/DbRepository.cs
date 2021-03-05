using System.Collections.Generic;
using System.Linq;
using MailSender.Data;
using MailSender.lib.Entities.Base;
using MailSender.lib.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Infrastructure.Services.InDatabase
{
    public class DbRepository<T> : IRepository<T>  where T : Entity
    {
        private readonly MailSenderDb _db;

        private DbSet<T> Set { get; }

        public DbRepository(MailSenderDb db)
        {
            _db = db;
            Set = db.Set<T>();
        }

        public IEnumerable<T> GetAll() => Set;

        //public T GetById(int id) => Set.FirstOrDefault(item => item.Id == id);
        public T GetById(int id) => Set.Find(id);

        public int Add(T item)
        {
            //Set.Add(item);
            _db.Entry(item).State = EntityState.Added;
            _db.SaveChanges();

            return item.Id;
        }

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public bool Remove(int id)
        {
            var item = GetById(id);
            if (item is null) return false;

            //Set.Remove(item);
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return true;
        }
    }
}
