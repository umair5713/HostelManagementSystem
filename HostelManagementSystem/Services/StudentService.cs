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

        public List<Student> SortByID()
        {
            _repo.SortByID();
            return _repo.GetStudents();
        }
    }
}
