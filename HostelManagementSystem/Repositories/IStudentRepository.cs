using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        List<Student> GetStudents();
        Student? GetById(int studentId);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        List<Student> GetSortedByID();
    }
}
