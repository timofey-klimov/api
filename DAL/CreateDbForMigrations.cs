using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL
{
    public class CreateDbForMigrations : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public CreateDbForMigrations()
        {

        }

        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-1O8U0H5\\SQLEXPRESS;Database=ApiDb;Trusted_Connection=True;");
            var databaseContext = new DatabaseContext(optionsBuilder.Options);

            return databaseContext;
        }
    }
}
