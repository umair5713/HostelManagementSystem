//namespace HostelManagementSystem.Models
//{
//    public class AdminDashboardViewModel
//    {
//        public int TotalStudents { get; set; }
//        public int AvailableRooms { get; set; }
//        public int PendingComplaints { get; set; }
//        public int FeesPaid { get; set; }

//        public Menu? TodaysMenu { get; set; }
//        public List<RecentActivity> RecentActivities { get; set; } = new();

//    }

//    public class RecentActivity
//    {
//        public string Title { get; set; } = string.Empty;
//        public string Description { get; set; } = string.Empty;
//        public string TimeAgo { get; set; } = string.Empty;
//        public string IconClass { get; set; } = string.Empty;
//        public string IconGradient { get; set; } = string.Empty;
//    }
//}



namespace HostelManagementSystem.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int AssignedStudents { get; set; }
        public int UnassignedStudents { get; set; }
        public int FeesPaid { get; set; }
        public int FeesPending { get; set; }
        public int PendingComplaints { get; set; }
        public Menu? TodaysMenu { get; set; }
        public List<RecentActivity> RecentActivities { get; set; } = new();
    }

    public class RecentActivity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public string IconGradient { get; set; } = string.Empty;
    }

    // Maps to vw_DashboardStats
    public class DashboardStatsResult
    {
        public int TotalStudents { get; set; }
        public int AssignedStudents { get; set; }
        public int UnassignedStudents { get; set; }
        public int FeesPaid { get; set; }
        public int FeesPending { get; set; }
    }
}