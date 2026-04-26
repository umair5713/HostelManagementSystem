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

namespace HostelManagementSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        // Register Student
        public void RegisterStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        // Get All Students
        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        // Get Student by ID
        public Student? GetById(int studentId)
        {
            return _context.Students
                .FirstOrDefault(s => s.StudentID == studentId);
        }

        // Update Student
        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        // Delete Student
        public void DeleteStudent(int studentId)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.StudentID == studentId);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        // Sort Students by ID
        public List<Student> SortById()
        {
            return _context.Students
                .OrderBy(s => s.StudentID)
                .ToList();
        }
    }
}
