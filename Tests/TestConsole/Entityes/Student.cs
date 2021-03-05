using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestConsole.Entityes
{
    //[Table("Studenti")]
    public class Student
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string LastName { get; set; }

        //[Column("MiddleName")]
        public string Patronymic { get; set; }

        public DateTime Birthday { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
