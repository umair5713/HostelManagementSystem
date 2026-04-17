using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    //public interface IComplaintRepository
    //{
    //    void Enqueue(Complaint complaint);
    //    Complaint? Dequeue();
    //    List<Complaint> GetQueue();
    //    bool IsEmpty();
    //    void UpdateStatus(int complaintId, string status);
    //    void AddComplaint(Complaint complaint);
    //    Complaint? GetById(int complaintId);
    //    List<Complaint> GetByStudent(string studentName);
    //}
    public interface IComplaintRepository
    {
        void AddComplaint(Complaint complaint);
        Complaint? GetById(int complaintId);
        List<Complaint> GetAll();
        List<Complaint> GetByStudent(string studentName);
        void UpdateStatus(int complaintId, string status);
        void DeleteComplaint(int complaintId);
    }
}