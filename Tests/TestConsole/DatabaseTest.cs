using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TestConsole.Data;
using TestConsole.Entityes;

namespace TestConsole
{
    internal static class DatabaseTest
    {
        private static async Task InitializeDBAsync(StudentsDb db)
        {
            //await db.Database.EnsureDeletedAsync();
            //await db.Database.EnsureCreatedAsync();
            await db.Database.MigrateAsync();

            if (!await db.Courses.AnyAsync())
            {
                var courses = Enumerable.Range(1, 10)
                   .Select(i => new Course { Name = $"Предмет {i}" })
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