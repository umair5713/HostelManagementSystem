using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using HostelManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;
        private readonly IComplaintService _complaintService;
        private readonly AppDbContext _context;
        private readonly IMenuService _menuService;

        public StudentController(
            IStudentService service,
            IComplaintService complaintService,
            AppDbContext context,
            IMenuService menuService)
        {
            _service = service;
            _complaintService = complaintService;
            _context = context;
            _menuService = menuService;
        }

      
        private string GetStudentName() =>
            HttpContext.Session.GetString("Username") ?? string.Empty;

        private int GetStudentId() =>
            HttpContext.Session.GetInt32("StudentID") ?? 0;

        public IActionResult Index()
        {
            var students = _service.GetAllStudents();
            return View("~/Views/Student/Index.cshtml", students);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Student/Register.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Student student)
        {
            _service.RegisterStudent(student);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var student = _service.GetById(id);
            if (student == null) return NotFound();
            return View("~/Views/Student/Details.cshtml", student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _service.GetById(id);
            if (student == null) return NotFound();
            return View("~/Views/Student/Edit.cshtml", student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            _service.UpdateStudent(student);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _service.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        public IActionResult SortByID()
        {
            var sorted = _service.SortById();
            return View("~/Views/Student/Index.cshtml", sorted);
        }


        public IActionResult Dashboard()
        {
            int studentId = GetStudentId();
            string studentName = GetStudentName();

            var student = _context.Students.FirstOrDefault(s => s.StudentID == studentId);
            var todaysMenu = _menuService.GetMenu(DateTime.Today);

            int totalDays = _context.AttendanceRecords.Any()
                ? (DateTime.Today - _context.AttendanceRecords.Min(a => a.Time).Date).Days + 1
                : 1;

            int attendanceCount = _context.AttendanceRecords
                                          .Count(a => a.StudentID == studentId);

            double attendancePercent = totalDays > 0
                ? Math.Round((attendanceCount / (double)totalDays) * 100, 1)
                : 0;

            int pendingComplaints = _context.Complaints
                .Count(c => c.StudentName == studentName && c.Status == "Pending");

            var fees = _context.Fees.Where(f => f.StudentID == studentId).ToList();
            int unpaidFees = fees.Count(f => !f.IsPaid);
            decimal totalDue = fees.Where(f => !f.IsPaid).Sum(f => f.Amount);

            var viewModel = new StudentDashboardViewModel
            {
                StudentID = student?.StudentID ?? 0,
                StudentName = student?.StudentName ?? studentName,
                Email = student?.Email ?? string.Empty,
                PhoneNumber = student?.PhoneNumber ?? string.Empty,
                CNIC = student?.CNIC ?? string.Empty,
                Semester = student?.Semester ?? 1,
                RoomNumber = student?.RoomNo ?? "Not Assigned",
                AttendancePercent = attendancePercent,
                CurrentDate = DateTime.Now,
                CurfewTime = "10:00 PM",

                TodaysMenu = todaysMenu != null
                    ? new List<MenuItem>
                    {
                new MenuItem { MealType = "Breakfast", Description = todaysMenu.Breakfast, Time = "Morning"   },
                new MenuItem { MealType = "Lunch",     Description = todaysMenu.Lunch,     Time = "Afternoon" },
                new MenuItem { MealType = "Dinner",    Description = todaysMenu.Dinner,    Time = "Evening"   }
                    }
                    : new List<MenuItem>(),

                Stats = new DashboardStats
                {
                    TotalAttendance = attendanceCount,
                    PendingComplaints = pendingComplaints,
                    OutstandingBills = totalDue,      
                    HostelBlock = "Block A"
                }
            };

            // ✅ Pass fee info to view via ViewBag
            ViewBag.UnpaidFees = unpaidFees;
            ViewBag.TotalDue = totalDue;
            ViewBag.TotalFees = fees.Count;

            return View("~/Views/Student/Dashboard.cshtml", viewModel);
        }


        public IActionResult MyComplaints()
        {
            string studentName = GetStudentName();
            var complaints = _complaintService.GetComplaintsByStudent(studentName);
            return View("~/Views/Student/MyComplaints.cshtml", complaints);
        }

        public IActionResult ComplaintDetails(int id)
        {
            var complaint = _complaintService.GetComplaintById(id);
            if (complaint == null) return NotFound();
            return View("~/Views/Student/ComplaintDetails.cshtml", complaint);
        }

        [HttpGet]
        public IActionResult SubmitComplaint()
        {
            return View("~/Views/Student/SubmitComplaint.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitComplaint(Complaint complaint)
        {
            complaint.StudentName = GetStudentName();
            _complaintService.AddComplaint(complaint);
            TempData["Success"] = "Complaint submitted successfully!";
            return RedirectToAction("MyComplaints");
        }
        public IActionResult Profile()
        {
            int studentId = GetStudentId();
            var student = _context.Students.FirstOrDefault(s => s.StudentID == studentId);

            if (student == null)
                return NotFound();

            var fees = _context.Fees.Where(f => f.StudentID == studentId).ToList();
            var unpaidFees = fees.Where(f => !f.IsPaid).ToList();

            ViewBag.TotalFees = fees.Count;
            ViewBag.UnpaidFees = unpaidFees.Count;
            ViewBag.HasUnpaid = unpaidFees.Any();

            return View("~/Views/Student/Profile.cshtml", student);
        }
    }
}