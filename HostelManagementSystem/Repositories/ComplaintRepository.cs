using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly AppDbContext _db;

        public ComplaintRepository(AppDbContext db)
        {
            _db = db;
        }

        // ADD
        public void AddComplaint(Complaint complaint)
        {
            _db.Database.ExecuteSqlRaw(
                @"INSERT INTO tbl_complaints (StudentName, Title, Description, Time, Status)
              VALUES ({0}, {1}, {2}, {3}, {4})",
                complaint.StudentName,
                complaint.Title,
                complaint.Description,
                DateTime.Now,
                complaint.Status ?? "Pending"
            );
        }

        // GET ALL
        public List<Complaint> GetAll()
        {
               return _db.Complaints
                      .FromSqlRaw("SELECT ComplaintID, StudentName, Title, Description, Time, Status FROM tbl_complaints ORDER BY ComplaintID ASC")
                      .ToList();
        }

        // GET BY ID
        public Complaint? GetById(int complaintId)
        {
            return _db.Complaints
                      .FromSqlRaw("SELECT ComplaintID, StudentName, Title, Description, Time, Status FROM tbl_complaints WHERE ComplaintID = {0}", complaintId)
                      .FirstOrDefault();
        }

        // GET BY STUDENT
        public List<Complaint> GetByStudent(string studentName)
        {
            return _db.Complaints
                      .FromSqlRaw("SELECT ComplaintID, StudentName, Title, Description, Time, Status FROM tbl_complaints WHERE StudentName = {0} ORDER BY ComplaintID ASC", studentName)
                      .ToList();
        }

        // UPDATE STATUS
        public void UpdateStatus(int complaintId, string status)
        {
            _db.Database.ExecuteSqlRaw(
                "UPDATE tbl_complaints SET Status = {0} WHERE ComplaintID = {1}",
                status,
                complaintId
            );
        }

        // DELETE
        public void DeleteComplaint(int complaintId)
        {
            _db.Database.ExecuteSqlRaw(
                "DELETE FROM tbl_complaints WHERE ComplaintID = {0}",
                complaintId
            );
        }
    }
}
