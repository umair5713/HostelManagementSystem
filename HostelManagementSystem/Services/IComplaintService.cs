using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IComplaintService
    {
        void SubmitComplaint(Complaint complaint);
        Complaint ProcessComplaint();
        List<Complaint> GetAllComplaints();
        bool IsEmpty();
    }
}
