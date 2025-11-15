using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class ComplaintRepository:IComplaintRepository
    {
        private ComplaintQueueNode? front;
        private ComplaintQueueNode? back;

        // ENQUEUE
        public void Enqueue(Complaint complaint)
        {
            ComplaintQueueNode newNode = new ComplaintQueueNode { Data = complaint, Next = null };

            if (front == null)
            {
                front = back = newNode;
            }
            else
            {
                back.Next = newNode;
                back = newNode;
            }
        }

        // DEQUEUE
        public Complaint Dequeue()
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
            ComplaintQueueNode temp = front;

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
    }
}
