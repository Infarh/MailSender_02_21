using System;
using System.Collections.Generic;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class SchedulerTask : Entity
    {
        public DateTime Time { get; set; }
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public List<Recipient> Recipients { get; set; }
        public Message Message { get; set; }
        //public List<Message> Messages { get; set; } = new ();
    }
}
