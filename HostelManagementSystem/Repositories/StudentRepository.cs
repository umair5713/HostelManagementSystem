using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }


        public void AddStudent(Student student)
        {
            _context.Database.ExecuteSqlRaw(
                @"INSERT INTO tbl_students (StudentName, Email, PhoneNumber, CNIC, Semester, RoomNo)
          VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                student.StudentName,
                student.Email,
                student.PhoneNumber,
                student.CNIC,
                student.Semester,
                student.RoomNo
            );
        }

       
        public List<Student> GetStudents()
        {
            return _context.Students
                      .FromSqlRaw("SELECT StudentID, StudentName,Email, PhoneNumber, CNIC, Semester, RoomNo  FROM tbl_students")
                      .ToList();
        }

       
        public Student? GetById(int studentId)
        {
            return _context.Students
                      .FromSqlRaw("SELECT StudentID, StudentName,Email, PhoneNumber, CNIC, Semester, RoomNo FROM tbl_students WHERE StudentID = {0}", studentId)
                      .FirstOrDefault();
        }

        
        public void UpdateStudent(Student student)
        {
            _context.Database.ExecuteSqlRaw(
                @"UPDATE tbl_students 
          SET StudentName  = {0},
              Email        = {1},
              PhoneNumber  = {2},
              CNIC         = {3},
              Semester     = {4},
              RoomNo       = {5}
          WHERE StudentID  = {6}",
                student.StudentName,
                student.Email,
                student.PhoneNumber,
                student.CNIC,
                student.Semester,
                student.RoomNo,
                student.StudentID
            );
        }

        // DELETE
        public void DeleteStudent(int studentId)
        {
            _context.Database.ExecuteSqlRaw(
                "DELETE FROM tbl_students WHERE StudentID = {0}",
                studentId
            );
        }

        // GET SORTED BY ID — replaces MergeSort
        public List<Student> GetSortedByID()
        {
            return _context.Students
                      .FromSqlRaw("SELECT StudentID, StudentName,Email, PhoneNumber, CNIC, Semester, RoomNo FROM tbl_students ORDER BY StudentID ASC")
                      .ToList();
        }

    }
}
