using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class Message : Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}