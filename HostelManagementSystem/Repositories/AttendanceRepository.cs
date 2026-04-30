using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;
        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }


        //public void AddAttendance(AttendanceRecord record)
        //{
        //    var student = _context.Students
        //        .FromSqlRaw("SELECT StudentID, StudentName,Email, PhoneNumber, CNIC, Semester, RoomNo, FeeStatus FROM tbl_students WHERE StudentID = {0}", record.StudentID)
        //        .FirstOrDefault();
        //    if (student == null)
        //        throw new Exception($"Student with ID {record.StudentID} does not exist in tbl_students.");

        //    _context.Database.ExecuteSqlRaw(
        //        @"INSERT INTO tbl_attendance (StudentID, StudentName, Time)
        //      VALUES ({0}, {1}, {2})",
        //        record.StudentID,
        //        record.StudentName,
        //        DateTime.Now
        //    );
        //}

        public void AddAttendance(AttendanceRecord record)
        {
            var student = _context.Students
                .FromSqlRaw(
                    "SELECT StudentID, StudentName, RoomNo, FeeStatus, Email, PhoneNumber, CNIC, Semester FROM tbl_students WHERE StudentID = {0}",
                    record.StudentID)
                .FirstOrDefault();

            if (student == null)
                throw new Exception($"Student with ID {record.StudentID} does not exist.");

            _context.Database.ExecuteSqlRaw(
                "INSERT INTO tbl_attendance (StudentID, StudentName, Time) VALUES ({0}, {1}, {2})",
                record.StudentID,
                record.StudentName,
                DateTime.Now
            );
        }


        public List<AttendanceRecord> GetAll()
        {
            return _context.AttendanceRecords
                      .FromSqlRaw("SELECT AttendanceID, StudentID, StudentName, Time FROM tbl_attendance ORDER BY Time DESC")
                      .ToList();
        }

        
        public List<AttendanceRecord> GetByStudent(int studentId)
        {
            return _context.AttendanceRecords
                      .FromSqlRaw("SELECT AttendanceID, StudentID, StudentName, Time FROM tbl_attendance WHERE StudentID = {0} ORDER BY Time DESC", studentId)
                      .ToList();
        }

        
        public AttendanceRecord? GetLatest(int studentId)
        {
            return _context.AttendanceRecords
                      .FromSqlRaw("SELECT TOP 1 AttendanceID, StudentID, StudentName, Time FROM tbl_attendance WHERE StudentID = {0} ORDER BY Time DESC", studentId)
                      .FirstOrDefault();
        }

        
        public bool HasAttendance(int studentId)
        {
            var result = _context.AttendanceRecords
                            .FromSqlRaw("SELECT TOP 1 AttendanceID, StudentID, StudentName, Time FROM tbl_attendance WHERE StudentID = {0}", studentId)
                            .FirstOrDefault();
            return result != null;
        }

        
        public int Count(int studentId)
        {
            return _context.AttendanceRecords
                      .FromSqlRaw("SELECT AttendanceID, StudentID, StudentName, Time FROM tbl_attendance WHERE StudentID = {0}", studentId)
                      .Count();
        }
    }
}
