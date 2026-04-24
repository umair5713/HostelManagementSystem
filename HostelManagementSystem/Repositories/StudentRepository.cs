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

        //public void AddStudent(Student student)
        //{
        //    _context.Database.ExecuteSqlRaw(
        //        @"INSERT INTO tbl_students (StudentName, RoomNo, FeeStatus)
        //      VALUES ({0}, {1}, {2})",
        //        student.StudentName,
        //        student.RoomNo,
        //        student.FeeStatus
        //    );
        //}

        public void AddStudent(Student student)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_RegisterStudent @StudentName, @RoomNo",
                new Microsoft.Data.SqlClient.SqlParameter("@StudentName", student.StudentName),
                new Microsoft.Data.SqlClient.SqlParameter("@RoomNo",
                    string.IsNullOrEmpty(student.RoomNo) ? DBNull.Value : student.RoomNo)
            );
        }

        // GET ALL
        public List<Student> GetStudents()
        {
            return _context.Students
                      .FromSqlRaw("SELECT StudentID, StudentName, RoomNo, FeeStatus FROM tbl_students")
                      .ToList();
        }

        // GET BY ID
        public Student? GetById(int studentId)
        {
            return _context.Students
                      .FromSqlRaw("SELECT StudentID, StudentName, RoomNo, FeeStatus FROM tbl_students WHERE StudentID = {0}", studentId)
                      .FirstOrDefault();
        }

        // UPDATE
        public void UpdateStudent(Student student)
        {
            _context.Database.ExecuteSqlRaw(
                @"UPDATE tbl_students 
              SET StudentName = {0}, RoomNo = {1}, FeeStatus = {2}
              WHERE StudentID = {3}",
                student.StudentName,
                student.RoomNo,
                student.FeeStatus,
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
                      .FromSqlRaw("SELECT StudentID, StudentName, RoomNo, FeeStatus FROM tbl_students ORDER BY StudentID ASC")
                      .ToList();
        }

    }
}
