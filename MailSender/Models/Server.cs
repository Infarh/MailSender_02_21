using System.ComponentModel.DataAnnotations;

using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Server : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public int Port { get; set; } = 25;

        public bool UseSSL { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public override string ToString() => $"{Name}:{Port}";
    }
}
