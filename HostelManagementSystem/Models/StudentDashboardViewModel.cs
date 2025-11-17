namespace HostelManagementSystem.Models
{
    public class StudentDashboardViewModel
    {
        public string StudentName { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CurrentDate { get; set; }
        public string CurfewTime { get; set; }
        public List<MenuItem> TodaysMenu { get; set; }
        public DashboardStats Stats { get; set; }
    }

    public class MenuItem
    {
        public string MealType { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
    }

    public class DashboardStats
    {
        public int TotalAttendance { get; set; }
        public int PendingComplaints { get; set; }
        public decimal OutstandingBills { get; set; }
        public string HostelBlock { get; set; }
    }
}