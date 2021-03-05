using System.ComponentModel.DataAnnotations;
using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class Sender : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
