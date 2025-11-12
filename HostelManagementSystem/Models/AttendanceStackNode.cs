namespace HostelManagementSystem.Models
{
    public class AttendanceStackNode
    {
        public AttendanceRecord Data { get; set; }
        public AttendanceStackNode next { get; set; }
    }
}
