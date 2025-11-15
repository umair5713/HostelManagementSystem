using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IComplaintRepository
    {
        void Enqueue(Complaint complaint);
        Complaint Dequeue();
        List<Complaint> GetQueue();
        bool IsEmpty();
    }
}
