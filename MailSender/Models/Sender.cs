using System.ComponentModel.DataAnnotations;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Sender : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
