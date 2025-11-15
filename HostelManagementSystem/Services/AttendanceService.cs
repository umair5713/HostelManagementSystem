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

        public void MarkAttendance(string studentName)
        {
            AttendanceRecord record = new AttendanceRecord
            {
                StudentName = studentName,
                Time = DateTime.Now
            };

            _repo.Push(record);
        }

        public AttendanceRecord UndoAttendance()
        {
            return _repo.Pop();
        }
    }
}
