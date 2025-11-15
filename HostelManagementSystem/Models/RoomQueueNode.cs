namespace HostelManagementSystem.Models
{
    public class RoomQueueNode
    {
        public Student Data { get; set; }
        public RoomQueueNode next { get; set; }
    }
}
