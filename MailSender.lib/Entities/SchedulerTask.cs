using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class SchedulerTask : Entity
    {
        public DateTime Time { get; set; } = DateTime.Now;

        [Required]
        public Server Server { get; set; }

        [Required]
        public Sender Sender { get; set; }

        public ICollection<Recipient> Recipients { get; set; }

        [Required]
        public Message Message { get; set; }
        //public List<Message> Messages { get; set; } = new ();
    }
}
