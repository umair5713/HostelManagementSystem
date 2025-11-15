using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IRoomAllocationRepository
    {
        void enqueue(Student student);
        Student dequeue();
        bool empty();
        List<Student> get_queue();
    }
}
