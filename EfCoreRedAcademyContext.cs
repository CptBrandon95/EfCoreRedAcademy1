using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRedAcademy1;

public partial class EfCoreRedAcademyContext : DbContext
{
    public EfCoreRedAcademyContext()
    {
    }

    public EfCoreRedAcademyContext(DbContextOptions<EfCoreRedAcademyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Filename=EfCoreRedAcademy.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasIndex(e => e.ProfessorsId, "IX_Classes_ProfessorsId");

            entity.HasOne(d => d.Professors).WithMany(p => p.Classes).HasForeignKey(d => d.ProfessorsId);

            entity.HasMany(d => d.Students).WithMany(p => p.Classes)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassStudent",
                    r => r.HasOne<Student>().WithMany().HasForeignKey("StudentsId"),
                    l => l.HasOne<Class>().WithMany().HasForeignKey("ClassesId"),
                    j =>
                    {
                        j.HasKey("ClassesId", "StudentsId");
                        j.ToTable("ClassStudent");
                        j.HasIndex(new[] { "StudentsId" }, "IX_ClassStudent_StudentsId");
                    });
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasIndex(e => e.AddressId, "IX_Professors_AddressId");

            entity.HasOne(d => d.Address).WithMany(p => p.Professors).HasForeignKey(d => d.AddressId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.AddressesId, "IX_Students_AddressesId");

            entity.HasOne(d => d.Addresses).WithMany(p => p.Students).HasForeignKey(d => d.AddressesId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
