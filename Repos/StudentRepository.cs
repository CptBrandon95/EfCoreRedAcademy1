using EfCoreRedAcademy1.Model;
using EfCoreRedAcademy1.Repository;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRedAcademy1.Repos
{
    public class StudentRepository : IStudentRepository
    {
        private readonly EfCoreAcademyDbContext _efCoreAcademyDbContext;

        /* 
         * To implement the repo interface
         * We need to have a repository class that will be implementing it
         * When creating the repo class we need to create a constructor which will instantiate the dbContext class by passing it through the constructor
        
         */


        public StudentRepository(EfCoreAcademyDbContext efCoreAcademyDbContext)
        {
            _efCoreAcademyDbContext = efCoreAcademyDbContext;
        }
        public async Task<int> CreateStudentAsync(Student student)
        {
           _efCoreAcademyDbContext.Students.Add(student);
            await _efCoreAcademyDbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await _efCoreAcademyDbContext.Students.FindAsync(studentId);

            if(student != null) 
            {
                _efCoreAcademyDbContext.Remove(student);
                await _efCoreAcademyDbContext.SaveChangesAsync();
            }
            throw new InvalidOperationException(message: "Id not found");
        }

        public async Task<List<Student>> GetAllStudents()
        {
           return await _efCoreAcademyDbContext.Students.ToListAsync(); 
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {

            return await _efCoreAcademyDbContext.Students.FindAsync(studentId);
        }

        public async Task UpdateStudentAsync(Student student)
        {
          _efCoreAcademyDbContext.Students.Attach(student);
            _efCoreAcademyDbContext.Entry(student).State = EntityState.Modified;
            await _efCoreAcademyDbContext.SaveChangesAsync();
        }
    }
}
