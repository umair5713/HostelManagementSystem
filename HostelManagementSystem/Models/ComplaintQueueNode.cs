namespace HostelManagementSystem.Models
{
    public class ComplaintQueueNode
    {
        public Complaint Data { get; set; } = null!;
        public ComplaintQueueNode? Next { get; set; }
    }
}