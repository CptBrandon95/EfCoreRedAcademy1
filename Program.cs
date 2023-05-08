using EfCoreRedAcademy1.GenRepo;
using EfCoreRedAcademy1.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EfCoreRedAcademy1
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var options = new DbContextOptionsBuilder<EfCoreAcademyDbContext>().UseSqlite().Options;
           var dbContext = new EfCoreAcademyDbContext(options);

            dbContext.Database.Migrate();

            // Inserting Data
            ProcessDelete();
            ProcessInsert();         
            ProcessSelect();
            ProcessUpdate();


            // removing data and making sure that the same data isnt adding twice

            void ProcessDelete()
            {
                dbContext = new EfCoreAcademyDbContext(options);

                // first we are loading the data
                var professor = dbContext.Professors.ToList();
                var students = dbContext.Students.ToList();
                var classes = dbContext.Classes.ToList();
                var addresses = dbContext.Address.ToList();

                // Now we are removing it so we are not duplicating the data

                dbContext.Professors.RemoveRange(professor);
                dbContext.Students.RemoveRange(students);
                dbContext.Classes.RemoveRange(classes);
                dbContext.Classes.RemoveRange(classes);

                dbContext.SaveChanges();
                dbContext.Dispose();

            }

            // Inserting data into the database

            void ProcessInsert()
            {
                dbContext = new EfCoreAcademyDbContext(options);

                var address = new Address() { City = "Hamburg", Street = "Demostreet", Zip = "2547", HouseNumber = 1 };
                var professor = new Professors() { FirstName = "Brandon", LastName = "Wiener", Address = address };
                var student1 = new Student() { FirstName = "John", LastName = "Doe", Addresses = address };
                var student2= new Student() { FirstName = "Jane", LastName = "Doe", Addresses = address };
                var class1 = new Class() { Professors = professor, Students = new List<Student> { student1, student2 }, Title="IT" };

                dbContext.Address.Add(address);
                dbContext.Professors.Add(professor);
                dbContext.Students.Add(student1);
                dbContext.Students.Add(student2);
                dbContext.Classes.Add(class1);


                dbContext.SaveChanges();
                dbContext.Dispose();

            }


            // Using Single and including 
            // filtering with a where clause
            void ProcessSelect()
            {
                dbContext = new EfCoreAcademyDbContext(options);

                var professor = dbContext.Professors.Include(p => p.Address).Single(p => p.FirstName == "Brandon");
                var student = dbContext.Students.Include(c => c.Classes).Where(s => s.FirstName =="Jane").ToList();
                dbContext.Dispose();
               
            }


            // Change tracking
            void ProcessUpdate()
            {
                dbContext = new EfCoreAcademyDbContext(options);

                var student = dbContext.Students.First();
                student.FirstName = "Tim";
                dbContext.SaveChanges();

                dbContext.Dispose();
                dbContext = new EfCoreAcademyDbContext(options);

                student = dbContext.Students.First();
                dbContext.Dispose();

            }

            async void ProcessRepository()
            {
                dbContext = new EfCoreAcademyDbContext(options);
                var repository = new GenericRepo<Student>(dbContext);

                // simple select
                var studnets = await repository.GetAsync(null, null);
                var student = await repository.GetByIdAsync(studnets.First().Id);

                // Includes
                student = await repository.GetByIdAsync(student.Id, (student) => student.Addresses,
                    (student => student.Classes));

                // filters
                Expression<Func<Student, bool>> filter = (student) => student.FirstName == "Jane";
                studnets = await repository.GetFilteredAsync(new[] { filter }, null, null);
                Console.ReadLine();
            }
            
          
        }
    }
}