using HostelManagementSystem.Models;
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

        public List<Student> GetAllStudents()
        {
            return _repo.GetStudents();
        }

        public void RegisterStudent(Student student)
        {
            _repo.AddStudent(student);
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
        public List<Student> SortById()
        {
            return _repo.GetStudents();
        }
    }
}
