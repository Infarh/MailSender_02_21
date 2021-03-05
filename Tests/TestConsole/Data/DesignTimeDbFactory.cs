using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestConsole.Data
{
    public class DesignTimeDbFactory : IDesignTimeDbContextFactory<StudentsDb>
    {
        public StudentsDb CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<StudentsDb>()
               .UseLazyLoadingProxies()
               .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB")
               .Options;

            return new StudentsDb(options);
        }
    }
}
