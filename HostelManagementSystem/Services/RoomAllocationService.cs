using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class RoomAllocationService: IRoomAllocationService
    {
        private readonly IRoomAllocationRepository _repo;


        public RoomAllocationService(IRoomAllocationRepository repo)
        {
            _repo = repo;
        }


        public void AddToQueue(Student student)
        {
            _repo.enqueue(student);
        }


        public Student AllocateRoom()
        {
            return _repo.dequeue();
        }


        public List<Student> GetQueue()
        {
            return _repo.get_queue();
        }


        public bool IsEmpty()
        {
            return _repo.empty();
        }
    }
}
