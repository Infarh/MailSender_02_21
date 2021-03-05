using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MailSender.lib.Entities.Base;

namespace MailSender.lib.Entities
{
    public class Recipient : Entity, IDataErrorInfo
    {
        private string _Name;

        [Required]
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

        [Required]
        public string Address { get; set; }

        string IDataErrorInfo.Error => null;

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    default: return null;

                    case nameof(Name):
                        var name = Name;

                        if (name is null) return "Имя не может быть пустой ссылкой";
                        if (name.Length < 3) return "Длина имени должна быть не менее 3 символов";
                        if (name.Length > 20) return "Длина имени должна быть не более 20 символов";

                        return null;
                }
            }
        }
    }
}