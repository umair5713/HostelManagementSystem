using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IAttendanceService
    {
        void MarkAttendance(int studentId, string studentName);
        List<AttendanceRecord> GetAll();
        List<AttendanceRecord> GetByStudent(int studentId);
        AttendanceRecord? GetLatest(int studentId);
        bool HasAttendance(int studentId);
        int GetCount(int studentId);
    }
}
