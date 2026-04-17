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

        public void AddComplaint(Complaint complaint)
        {
            complaint.Time = DateTime.Now;
            complaint.Status = "Pending";
            _repo.AddComplaint(complaint);
        }

        public List<Complaint> GetAllComplaints()
        {
            return _repo.GetAll();
        }

        public Complaint? GetComplaintById(int complaintId)
        {
            return _repo.GetById(complaintId);
        }

        public List<Complaint> GetComplaintsByStudent(string studentName)
        {
            return _repo.GetByStudent(studentName);
        }

        public void UpdateComplaintStatus(int complaintId, string status)
        {
            _repo.UpdateStatus(complaintId, status);
        }

        public void DeleteComplaint(int complaintId)
        {
            _repo.DeleteComplaint(complaintId);
        }
    }
}
