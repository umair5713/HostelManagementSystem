namespace HostelManagementSystem.Models
{
    public class Complaint
    {
        public int ComplaintID { get; set; }
        public string StudentName { get; set; }=string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Time { get; set; }
    }
}
