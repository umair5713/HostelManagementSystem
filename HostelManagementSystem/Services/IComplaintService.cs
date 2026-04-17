using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IComplaintService
    {
        void AddComplaint(Complaint complaint);
        List<Complaint> GetAllComplaints();
        Complaint? GetComplaintById(int complaintId);
        List<Complaint> GetComplaintsByStudent(string studentName);
        void UpdateComplaintStatus(int complaintId, string status);
        void DeleteComplaint(int complaintId);
        
    }
}