using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IAttendance_Repository
    {
        void Push(AttendanceRecord record);
        AttendanceRecord Pop();
        List<AttendanceRecord> GetAttendanceList();
        AttendanceRecord Peek();
        bool IsEmpty();
        int Count();
        bool Search(string name);

    }
}
