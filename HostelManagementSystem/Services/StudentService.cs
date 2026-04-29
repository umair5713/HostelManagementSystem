//using HostelManagementSystem.Models;

//namespace HostelManagementSystem.Services
//{
//    public interface IStudentService
//    {
//        void RegisterStudent(Student student);
//        List<Student> GetAllStudents();
//        Student? GetById(int studentId);
//        void UpdateStudent(Student student);
//        void DeleteStudent(int studentId);
//        List<Student> SortById();
//    }
//}
using HostelManagementSystem.Models;
using HostelManagementSystem.Data;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        // Register Student
        public void RegisterStudent(Student student)
        {
            _repo.AddStudent(student);
        }

        public List<Student> GetAllStudents()
        {
            return _repo.GetStudents();
        }

        public Student? GetById(int studentId)
        {
            return _repo.GetById(studentId);
        }

        public void UpdateStudent(Student student)
        {
            _repo.UpdateStudent(student);
        }

        public void DeleteStudent(int studentId)
        {
            _repo.DeleteStudent(studentId);
        }

        // Sort Students by ID
        public List<Student> SortById()
        {
            return _repo.GetSortedByID();
        }
    }
}
