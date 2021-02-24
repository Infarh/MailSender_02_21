using System;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Recipient : Entity
    {
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Не задано имя!");

                _Name = value;
            }
        }

        public string Address { get; set; }
    }
}