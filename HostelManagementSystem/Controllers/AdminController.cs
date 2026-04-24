//using HostelManagementSystem.Models;
//using HostelManagementSystem.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace HostelManagementSystem.Controllers
//{
//    public class AdminController : Controller
//    {
//        private readonly IStudentService _studentService;
//        private readonly IComplaintService _complaintService;
//        private readonly IMenuService _menuService;

//        public AdminController(IStudentService studentService, IComplaintService complaintService, IMenuService menuService)

//        {
//            _studentService = studentService;
//            _complaintService = complaintService;
//            _menuService = menuService;
//        }
//        public IActionResult Dashboard()
//        {
//            var students = _studentService.GetAllStudents();
//            var complaints = _complaintService.GetAllComplaints();
//            var todaysMenu = _menuService.GetMenu(DateTime.Today);

//            // Build recent activities from latest complaints and newest students
//            var recentActivities = new List<RecentActivity>();

//            // Add latest 2 complaints as activities
//            var recentComplaints = complaints.OrderByDescending(c => c.Time).Take(2);

//            foreach (var c in recentComplaints)
//            {
//                recentActivities.Add(new RecentActivity
//                {
//                    Title = "New Complaint Filed",
//                    Description = $"{c.Title} - by {c.StudentName}",
//                    TimeAgo = GetTimeAgo(c.Time),
//                    IconClass = "fas fa-exclamation-circle",
//                    IconGradient = "linear-gradient(135deg, #f093fb, #f5576c)"
//                });
//            }

//            // Add latest 1 student registration as activity
//            var latestStudent = students.LastOrDefault();
//            if (latestStudent != null)
//            {
//                recentActivities.Add(new RecentActivity
//                {
//                    Title = "New Student Registered",
//                    Description = $"{latestStudent.StudentName} assigned to Room {latestStudent.RoomNo ?? "N/A"}",
//                    TimeAgo = "Recently",
//                    IconClass = "fas fa-user-plus",
//                    IconGradient = "linear-gradient(135deg, #667eea, #764ba2)"
//                });
//            }

//            var viewModel = new AdminDashboardViewModel
//            {
//                TotalStudents = students.Count,
//                AvailableRooms = students.Count(s => string.IsNullOrEmpty(s.RoomNo)),
//                PendingComplaints = complaints.Count(c => c.Status == "Pending"),
//                FeesPaid = students.Count(s => s.FeeStatus == true),
//                TodaysMenu = todaysMenu,
//                RecentActivities = recentActivities
//            };

//            return View(viewModel);
//        }

//        // Helper: converts DateTime to "X hours ago" style
//        private string GetTimeAgo(DateTime time)
//        {
//            var diff = DateTime.Now - time;
//            if (diff.TotalMinutes < 1) return "Just now";
//            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} minutes ago";
//            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} hours ago";
//            return $"{(int)diff.TotalDays} days ago";
//        }
//    }
//}

using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using HostelManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IComplaintService _complaintService;
        private readonly IMenuService _menuService;
        private readonly AppDbContext _context;

        public AdminController(
            IStudentService studentService,
            IComplaintService complaintService,
            IMenuService menuService,
            AppDbContext context)
        {
            _studentService = studentService;
            _complaintService = complaintService;
            _menuService = menuService;
            _context = context;
        }

        public IActionResult Dashboard()
        {
            // ── Call sp_GetDashboardStats ──────────────────────────
            var stats = _context.Database
                .SqlQueryRaw<DashboardStatsResult>(
                    "EXEC sp_GetDashboardStats")
                .AsEnumerable()
                .FirstOrDefault();

            // ── Get pending complaints count ───────────────────────
            var pendingComplaints = _complaintService
                .GetAllComplaints()
                .Count(c => c.Status == "Pending");

            // ── Get today's menu ───────────────────────────────────
            var todaysMenu = _menuService.GetMenu(DateTime.Today);

            // ── Build recent activities ────────────────────────────
            var complaints = _complaintService.GetAllComplaints();
            var recentActivities = new List<RecentActivity>();

            var recentComplaints = complaints
                .OrderByDescending(c => c.Time)
                .Take(2);

            foreach (var c in recentComplaints)
            {
                recentActivities.Add(new RecentActivity
                {
                    Title = "New Complaint Filed",
                    Description = $"{c.Title} - by {c.StudentName}",
                    TimeAgo = GetTimeAgo(c.Time),
                    IconClass = "fas fa-exclamation-circle",
                    IconGradient = "linear-gradient(135deg, #f093fb, #f5576c)"
                });
            }

            var latestStudent = _studentService.GetAllStudents().LastOrDefault();
            if (latestStudent != null)
            {
                recentActivities.Add(new RecentActivity
                {
                    Title = "New Student Registered",
                    Description = $"{latestStudent.StudentName} in Room {latestStudent.RoomNo ?? "N/A"}",
                    TimeAgo = "Recently",
                    IconClass = "fas fa-user-plus",
                    IconGradient = "linear-gradient(135deg, #667eea, #764ba2)"
                });
            }

            // ── Build ViewModel ────────────────────────────────────
            var viewModel = new AdminDashboardViewModel
            {
                TotalStudents = stats?.TotalStudents ?? 0,
                AssignedStudents = stats?.AssignedStudents ?? 0,
                UnassignedStudents = stats?.UnassignedStudents ?? 0,
                FeesPaid = stats?.FeesPaid ?? 0,
                FeesPending = stats?.FeesPending ?? 0,
                PendingComplaints = pendingComplaints,
                TodaysMenu = todaysMenu,
                RecentActivities = recentActivities
            };

            return View(viewModel);
        }

        private string GetTimeAgo(DateTime time)
        {
            var diff = DateTime.Now - time;
            if (diff.TotalMinutes < 1) return "Just now";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} minutes ago";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} hours ago";
            return $"{(int)diff.TotalDays} days ago";
        }
    }
}
