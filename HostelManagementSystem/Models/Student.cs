namespace HostelManagementSystem.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string RoomNo { get; set; }
        public bool FeeStatus { get; set; }
        public List<string> AttendanceRecords { get; set; }
    }
}
