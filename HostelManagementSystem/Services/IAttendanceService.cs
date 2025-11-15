using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IAttendanceService
    {
        List<AttendanceRecord> GetAll();
        void MarkAttendance(string studentName);
        AttendanceRecord UndoAttendance();
    }
}
