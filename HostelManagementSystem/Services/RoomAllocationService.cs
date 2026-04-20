using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class RoomAllocationService : IRoomAllocationService
    {
        private readonly IRoomAllocationRepository _repo;

        public RoomAllocationService(IRoomAllocationRepository repo)
        {
            _repo = repo;
        }

        public void AllocateRoom(int studentId, string roomNo)
        {
            if (_repo.IsRoomTaken(roomNo))
                throw new Exception($"Room {roomNo} is already occupied.");

            _repo.AllocateRoom(studentId, roomNo);
        }

        public void DeallocateRoom(int studentId)
        {
            _repo.DeallocateRoom(studentId);
        }

        public List<Student> GetAllRooms()
        {
            return _repo.GetAllRooms();
        }

        public Student? GetByRoom(string roomNo)
        {
            return _repo.GetByRoom(roomNo);
        }

        public bool IsRoomTaken(string roomNo)
        {
            return _repo.IsRoomTaken(roomNo);
        }
    }
}
