using EfCoreRedAcademy1.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRedAcademy1
{
    public class EfCoreAcademyDbContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professors> Professors { get; set; }
        public DbSet<Class> Classes { get; set; }

        public EfCoreAcademyDbContext(DbContextOptions<EfCoreAcademyDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=EfCoreRedAcademy.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasKey(e => e.Id);
            modelBuilder.Entity<Student>().HasKey(e => e.Id);
            modelBuilder.Entity<Professors>().HasKey(e => e.Id);
            modelBuilder.Entity<Class>().HasKey(e => e.Id);

            modelBuilder.Entity<Student>().HasOne(e => e.Addresses);
            modelBuilder.Entity<Professors>().HasOne(e => e.Address);

            modelBuilder.Entity<Class>().HasMany(e => e.Students).WithMany(e => e.Classes);
            modelBuilder.Entity<Class>().HasOne(e => e.Professors).WithMany(e => e.Classes);

        }

    }
}
