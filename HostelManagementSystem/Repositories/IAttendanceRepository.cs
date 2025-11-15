using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IAttendanceRepository
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
