using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        void RegisterStudent(Student student);
        List<Student> SortByID();
    }
}
