using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class AttendanceService:IAttendanceService
    {
        private readonly IAttendanceRepository _repo;

        public AttendanceService(IAttendanceRepository repo)
        {
            _repo = repo;
        }

        public List<AttendanceRecord> GetAll()
        {
            return _repo.GetAttendanceList();
        }

        public void MarkAttendance(string studentId,string studentName)
        {
            AttendanceRecord record = new AttendanceRecord
            {
                StudentID = studentId,
                StudentName = studentName,
                Time = DateTime.Now
            };

            _repo.Push(record);
        }

        public AttendanceRecord UndoAttendance()
        {
            return _repo.Pop();
        }

        public List<AttendanceRecord> GetAttendanceByStudentId(string studentId)
        {
            //return attendanceRecords.Where(r => r.StudentId == studentId).ToList();
            var allRecords = _repo.GetAttendanceList();
            return allRecords.Where(r => r.StudentID== studentId).ToList();
        }
    }
}
