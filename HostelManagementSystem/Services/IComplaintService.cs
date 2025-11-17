using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IComplaintService
    {
        void SubmitComplaint(Complaint complaint);
        Complaint? ProcessComplaint();
        List<Complaint> GetAllComplaints();
        bool IsEmpty();
        void UpdateComplaintStatus(int complaintId, string status);
        void AddComplaint(Complaint complaint);
        Complaint? GetComplaintById(int complaintId);
        List<Complaint> GetComplaintsByStudent(string studentName);
    }
}