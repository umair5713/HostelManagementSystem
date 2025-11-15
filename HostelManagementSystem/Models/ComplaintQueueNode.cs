namespace HostelManagementSystem.Models
{
    public class ComplaintQueueNode
    {
        public Complaint Data { get; set; }
        public ComplaintQueueNode Next { get; set; }
    }
}
