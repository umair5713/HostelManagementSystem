using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IAttendanceService
    {
        List<AttendanceRecord> GetAll();
        void MarkAttendance(string studentId, string studentName);
        AttendanceRecord UndoAttendance();

        List<AttendanceRecord> GetAttendanceByStudentId(string studentId);
    }
}
