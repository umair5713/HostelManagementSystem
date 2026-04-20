using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IRoomAllocationRepository
    {
        void AllocateRoom(int studentId, string roomNo);
        void DeallocateRoom(int studentId);
        List<Student> GetAllRooms();
        Student? GetByRoom(string roomNo);
        bool IsRoomTaken(string roomNo);
    }
}
