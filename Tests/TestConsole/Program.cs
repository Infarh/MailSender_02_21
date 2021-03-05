using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TestConsole.Data;
using TestConsole.Entityes;

namespace TestConsole
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<StudentsDb>()
               .UseLazyLoadingProxies()
               .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB")
               .Options;

            using (var db = new StudentsDb(options)) 
                await InitializeDBAsync(db);

            using (var db = new StudentsDb(options))
            {
                var ivanov = await db.Students.FindAsync(99999);

                var query = db.Students
                   //.Include(s => s.Courses)
                   .Where(s => s.Birthday >= new DateTime(2000, 1, 1) && s.Birthday < new DateTime(2001, 1, 1));
                var students = await query.ToArrayAsync();

                var first_student = students[0];
                var courses = first_student.Courses;

                var students_count = await query.CountAsync();
                var students_count2 = await db.Students
                   .CountAsync(s => s.Birthday >= new DateTime(2000, 1, 1) && s.Birthday < new DateTime(2001, 1, 1));

                var students_last_names = await query.Select(s => s.LastName).Distinct().ToArrayAsync();

                foreach (var student in students)
                {
                    Console.WriteLine("{0} {1} {2} - {3:d}", student.LastName, student.Name, student.Patronymic, student.Birthday);

                    //db.Entry(student).Property(s => s.Courses).
                }
            }

            Console.WriteLine("Завершено");
            Console.ReadLine();
        }

        private static async Task InitializeDBAsync(StudentsDb db)
        {
            //await db.Database.EnsureDeletedAsync();
            //await db.Database.EnsureCreatedAsync();
            await db.Database.MigrateAsync();

            if (!await db.Courses.AnyAsync())
            {
                var courses = Enumerable.Range(1, 10)
                   .Select(i => new Course {Name = $"Предмет {i}"})
                   .ToArray();

                await db.Courses.AddRangeAsync(courses);

                await db.SaveChangesAsync();

                if (!await db.Students.AnyAsync())
                {
                    var rnd = new Random();
                    var students = Enumerable.Range(1, 1000)
                       .Select(
                            i => new Student
                            {
                                Name = $"Имя-{i}",
                                LastName = $"Фамилия-{i}",
                                Patronymic = $"Отчество-{i}",
                                Birthday = DateTime.Now.Date.AddYears(-rnd.Next(17, 28)).AddDays(rnd.Next(365)),
                                Courses = Enumerable.Range(0, rnd.Next(1, 8))
                                   .Select(_ => courses[rnd.Next(courses.Length)])
                                   .Distinct()
                                   .ToArray()
                            });
                    await db.Students.AddRangeAsync(students);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
