using System.Linq;
using MailSender.Models;

namespace MailSender.Infrastructure.Services.InMemory
{
    public class MessagesRepository : RepositoryInMemory<Message>
    {
        public MessagesRepository() : base(Enumerable.Range(1, 10)
           .Select(i => new Message
            {
                Id = i,
                Title = $"Заголовок сообщения - {i}",
                Text = $"Текст сообщения - {i}"
            }))
        {
        }

        public override void Update(Message item)
        {
            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Title = item.Title;
            db_item.Text = item.Text;
        }
    }
}