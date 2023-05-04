using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EfCoreAcademy
{
    public class EfCoreAcademyDbContextFactory : IDesignTimeDbContextFactory<EfCoreAcademyDbContext>
    {
        public EfCoreAcademyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfCoreAcademyDbContext>();
            optionsBuilder.UseSqlite("Filename=EfCoreAcdemy.db");

            return new EfCoreAcademyDbContext(optionsBuilder.Options);  
        }
    }
}
