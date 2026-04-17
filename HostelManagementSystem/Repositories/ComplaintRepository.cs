using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    //public class ComplaintRepository : IComplaintRepository
    //{
    //    private ComplaintQueueNode? front;
    //    private ComplaintQueueNode? back;
    //    private int nextId = 1;

    //    // ENQUEUE
    //    public void Enqueue(Complaint complaint)
    //    {
    //        complaint.ComplaintID = nextId++;
    //        ComplaintQueueNode newNode = new ComplaintQueueNode { Data = complaint, Next = null };

    //        if (front == null)
    //        {
    //            front = back = newNode;
    //        }
    //        else
    //        {
    //            if (back != null)
    //            {
    //                back.Next = newNode;
    //            }
    //            back = newNode;
    //        }
    //    }

    //    // DEQUEUE
    //    public Complaint? Dequeue()
    //    {
    //        if (front == null)
    //            return null;

    //        Complaint removed = front.Data;
    //        front = front.Next;

    //        if (front == null)
    //            back = null;

    //        return removed;
    //    }

    //    // GET LIST
    //    public List<Complaint> GetQueue()
    //    {
    //        List<Complaint> list = new List<Complaint>();
    //        ComplaintQueueNode? temp = front;

    //        while (temp != null)
    //        {
    //            list.Add(temp.Data);
    //            temp = temp.Next;
    //        }

    //        return list;
    //    }

    //    // EMPTY CHECK
    //    public bool IsEmpty()
    //    {
    //        return front == null;
    //    }

    //    // UPDATE STATUS
    //    public void UpdateStatus(int complaintId, string status)
    //    {
    //        ComplaintQueueNode? temp = front;

    //        while (temp != null)
    //        {
    //            if (temp.Data.ComplaintID == complaintId)
    //            {
    //                temp.Data.Status = status;
    //                break;
    //            }
    //            temp = temp.Next;
    //        }
    //    }

    //    // GET BY ID
    //    public Complaint? GetById(int complaintId)
    //    {
    //        ComplaintQueueNode? temp = front;

    //        while (temp != null)
    //        {
    //            if (temp.Data.ComplaintID == complaintId)
    //            {
    //                return temp.Data;
    //            }
    //            temp = temp.Next;
    //        }

    //        return null;
    //    }

    //    // GET BY STUDENT
    //    public List<Complaint> GetByStudent(string studentName)
    //    {
    //        List<Complaint> list = new List<Complaint>();
    //        ComplaintQueueNode? temp = front;

    //        while (temp != null)
    //        {
    //            if (temp.Data.StudentName.Equals(studentName, StringComparison.OrdinalIgnoreCase))
    //            {
    //                list.Add(temp.Data);
    //            }
    //            temp = temp.Next;
    //        }

    //        return list;
    //    }

    //    // ✅ ADD NEW COMPLAINT DIRECTLY
    //    public void AddComplaint(Complaint complaint)
    //    {
    //        // Generate new ID
    //        int maxId = 0;
    //        ComplaintQueueNode? current = front;

    //        while (current != null)
    //        {
    //            if (current.Data.ComplaintID > maxId)
    //                maxId = current.Data.ComplaintID;
    //            current = current.Next;
    //        }

    //        complaint.ComplaintID = maxId + 1;

    //        // Add to queue
    //        ComplaintQueueNode newNode = new ComplaintQueueNode { Data = complaint, Next = null };

    //        if (front == null)
    //        {
    //            front = back = newNode;
    //        }
    //        else
    //        {
    //            back!.Next = newNode;
    //            back = newNode;
    //        }
    //    }
    //}

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
