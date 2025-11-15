using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _repo;

        public ComplaintService(IComplaintRepository repo)
        {
            _repo = repo;
        }

        public void SubmitComplaint(Complaint complaint)
        {
            complaint.Time = DateTime.Now;
            _repo.Enqueue(complaint);
        }

        public Complaint ProcessComplaint()
        {
            return _repo.Dequeue();
        }

        public List<Complaint> GetAllComplaints()
        {
            return _repo.GetQueue();
        }

        public bool IsEmpty()
        {
            return _repo.IsEmpty();
        }
    }
}
