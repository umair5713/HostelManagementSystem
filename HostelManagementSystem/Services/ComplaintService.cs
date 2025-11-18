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
            complaint.Status = "Pending";
            _repo.Enqueue(complaint);
        }

        public Complaint? ProcessComplaint()
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

        public void UpdateComplaintStatus(int complaintId, string status)
        {
            _repo.UpdateStatus(complaintId, status);
        }

        public Complaint? GetComplaintById(int complaintId)
        {
            return _repo.GetById(complaintId);
        }

        public List<Complaint> GetComplaintsByStudent(string studentName)
        {
            return _repo.GetByStudent(studentName);
        }

        // ✅ New method to add a complaint directly
        public void AddComplaint(Complaint complaint)
        {
            _repo.AddComplaint(complaint);
        }
    }
}
