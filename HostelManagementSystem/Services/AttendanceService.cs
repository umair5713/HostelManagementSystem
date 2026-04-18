using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repo;

        public AttendanceService(IAttendanceRepository repo)
        {
            _repo = repo;
        }

        public void MarkAttendance(int studentId, string studentName)
        {
            AttendanceRecord record = new AttendanceRecord
            {
                StudentID = studentId,
                StudentName = studentName,
                Time = DateTime.Now
            };

            _repo.AddAttendance(record);
        }

        public List<AttendanceRecord> GetAll()
        {
            return _repo.GetAll();
        }

        public List<AttendanceRecord> GetByStudent(int studentId)
        {
            return _repo.GetByStudent(studentId);
        }

        public AttendanceRecord? GetLatest(int studentId)
        {
            return _repo.GetLatest(studentId);
        }

        public bool HasAttendance(int studentId)
        {
            return _repo.HasAttendance(studentId);
        }

        public int GetCount(int studentId)
        {
            return _repo.Count(studentId);
        }
    }
}
