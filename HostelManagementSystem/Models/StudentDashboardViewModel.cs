namespace HostelManagementSystem.Models
{
    public class StudentDashboardViewModel
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string CNIC { get; set; } = string.Empty;
        public int Semester { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public double AttendancePercent { get; set; }
        public DateTime CurrentDate { get; set; }
        public string CurfewTime { get; set; } = string.Empty;
        public List<MenuItem> TodaysMenu { get; set; } = new();
        public DashboardStats Stats { get; set; } = new();
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