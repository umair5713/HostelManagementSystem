using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IAttendanceRepository
    {
        void AddAttendance(AttendanceRecord record);
        List<AttendanceRecord> GetAll();
        List<AttendanceRecord> GetByStudent(int studentid);
        AttendanceRecord GetLatest(int studentid);
        bool HasAttendance(int studentid);
        int Count(int studentid);
        

    }
}
