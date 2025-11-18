using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private ComplaintQueueNode? front;
        private ComplaintQueueNode? back;
        private int nextId = 1;

        // ENQUEUE
        public void Enqueue(Complaint complaint)
        {
            complaint.ComplaintID = nextId++;
            ComplaintQueueNode newNode = new ComplaintQueueNode { Data = complaint, Next = null };

            if (front == null)
            {
                front = back = newNode;
            }
            else
            {
                if (back != null)
                {
                    back.Next = newNode;
                }
                back = newNode;
            }
        }

        // DEQUEUE
        public Complaint? Dequeue()
        {
            if (front == null)
                return null;

            Complaint removed = front.Data;
            front = front.Next;

            if (front == null)
                back = null;

            return removed;
        }

        // GET LIST
        public List<Complaint> GetQueue()
        {
            List<Complaint> list = new List<Complaint>();
            ComplaintQueueNode? temp = front;

            while (temp != null)
            {
                list.Add(temp.Data);
                temp = temp.Next;
            }

            return list;
        }

        // EMPTY CHECK
        public bool IsEmpty()
        {
            return front == null;
        }

        // UPDATE STATUS
        public void UpdateStatus(int complaintId, string status)
        {
            ComplaintQueueNode? temp = front;

            while (temp != null)
            {
                if (temp.Data.ComplaintID == complaintId)
                {
                    temp.Data.Status = status;
                    break;
                }
                temp = temp.Next;
            }
        }

        // GET BY ID
        public Complaint? GetById(int complaintId)
        {
            ComplaintQueueNode? temp = front;

            while (temp != null)
            {
                if (temp.Data.ComplaintID == complaintId)
                {
                    return temp.Data;
                }
                temp = temp.Next;
            }

            return null;
        }

        // GET BY STUDENT
        public List<Complaint> GetByStudent(string studentName)
        {
            List<Complaint> list = new List<Complaint>();
            ComplaintQueueNode? temp = front;

            while (temp != null)
            {
                if (temp.Data.StudentName.Equals(studentName, StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(temp.Data);
                }
                temp = temp.Next;
            }

            return list;
        }

        // ✅ ADD NEW COMPLAINT DIRECTLY
        public void AddComplaint(Complaint complaint)
        {
            // Generate new ID
            int maxId = 0;
            ComplaintQueueNode? current = front;

            while (current != null)
            {
                if (current.Data.ComplaintID > maxId)
                    maxId = current.Data.ComplaintID;
                current = current.Next;
            }

            complaint.ComplaintID = maxId + 1;

            // Add to queue
            ComplaintQueueNode newNode = new ComplaintQueueNode { Data = complaint, Next = null };

            if (front == null)
            {
                front = back = newNode;
            }
            else
            {
                back!.Next = newNode;
                back = newNode;
            }
        }
    }
}
