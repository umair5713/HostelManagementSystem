using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IStudentService
    {
        void RegisterStudent(Student student);
        List<Student> GetAllStudents();
        Student? GetById(int studentId);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        List<Student> SortById();
    }

}
