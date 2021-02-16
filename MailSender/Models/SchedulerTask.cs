using System;
using System.Collections.Generic;

namespace MailSender.Models
{
    public class SchedulerTask
    {
        public DateTime Time { get; set; }
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public List<Recipient> Recipients { get; set; }
        public Message Message { get; set; }
        //public List<Message> Messages { get; set; } = new ();
    }
}
