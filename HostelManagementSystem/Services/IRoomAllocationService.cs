using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IRoomAllocationService
    {
        void AddToQueue(Student student);
        Student AllocateRoom();
        List<Student> GetQueue();
        bool IsEmpty();
    }
}
