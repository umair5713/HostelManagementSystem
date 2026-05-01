using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using HostelManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
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
            // ── Call sp_GetDashboardStats — one DB call for all stats ──
            var stats = _context.Database
                .SqlQueryRaw<DashboardStatsResult>("EXEC sp_GetDashboardStats")
                .AsEnumerable()
                .FirstOrDefault();

            // ── Get all complaints once ────────────────────────────────
            var complaints = _complaintService.GetAllComplaints();

            // ── Pending count from same list — no extra DB call ────────
            var pendingComplaints = complaints.Count(c => c.Status == "Pending");

            // ── Get today's menu ───────────────────────────────────────
            var todaysMenu = _menuService.GetMenu(DateTime.Today);

            Console.WriteLine($"Today: {DateTime.Today}");
            Console.WriteLine($"Menu found: {todaysMenu != null}");
            Console.WriteLine($"Menu ID: {todaysMenu?.MenuID}");

            // ── Build recent activities ────────────────────────────────
            var recentActivities = new List<RecentActivity>();

            // Recent complaints
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

            // Latest registered student
            var latestStudent = _studentService.GetAllStudents().LastOrDefault();
            if (latestStudent != null)
            {
                recentActivities.Add(new RecentActivity
                {
                    Title = "New Student Registered",
                    Description = $"{latestStudent.StudentName} in Room {(string.IsNullOrEmpty(latestStudent.RoomNo) ? "Not Assigned" : latestStudent.RoomNo)}",
                    TimeAgo = "Recently",
                    IconClass = "fas fa-user-plus",
                    IconGradient = "linear-gradient(135deg, #667eea, #764ba2)"
                });
            }

            // ── Build ViewModel ────────────────────────────────────────
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

        // ── HELPER ────────────────────────────────────────────────────
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