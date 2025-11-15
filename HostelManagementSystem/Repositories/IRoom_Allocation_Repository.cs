using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IRoom_Allocation_Repository
    {
        void enqueue(Student student);
        Student dequeue();
        bool empty();
        List<Student> get_queue();
    }
}
