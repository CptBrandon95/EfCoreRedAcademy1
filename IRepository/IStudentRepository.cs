using EfCoreRedAcademy1.Model;

namespace EfCoreRedAcademy1.Repository
{
    public interface IStudentRepository
    {
        // Creating a new student
        Task<int> CreateStudentAsync(Student student);
        
        // Updating a student
        Task UpdateStudentAsync(Student student);
        
        // Get student by Id
        Task<Student?> GetStudentByIdAsync(int studentId);
        
        // Get all students
        Task<List<Student>> GetAllStudents();
        
        // Delete student
        Task DeleteStudentAsync(int studentId);
    }
}
