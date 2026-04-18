using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;
        private readonly IComplaintService _complaintService;

        public StudentController(IStudentService service, IComplaintService complaintService)
        {
            _service = service;
            _complaintService = complaintService;
        }

        // ADMIN: View all students
        public IActionResult Index()
        {
            var students = _service.GetAllStudents();
            return View("~/Views/Student/Index.cshtml", students);
        }

        // ADMIN: Register student (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Student/Register.cshtml");
        }

        // ADMIN: Register student (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Student student)
        {
            _service.RegisterStudent(student);
            return RedirectToAction("Index");
        }

        // ADMIN: View student details
        public IActionResult Details(int id)
        {
            var student = _service.GetById(id);
            if (student == null)
                return NotFound();

            return View("~/Views/Student/Details.cshtml", student);
        }

        // ADMIN: Edit student (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _service.GetById(id);
            if (student == null)
                return NotFound();

            return View("~/Views/Student/Edit.cshtml", student);
        }

        // ADMIN: Edit student (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            _service.UpdateStudent(student);
            return RedirectToAction("Index");
        }

        // ADMIN: Delete student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _service.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        // ADMIN: Sort students by ID
        public IActionResult SortByID()
        {
            var sorted = _service.SortById();
            return View("~/Views/Student/Index.cshtml", sorted);
        }

        // STUDENT: Dashboard
        public IActionResult Dashboard()
        {
            string studentName = User.Identity?.Name ?? string.Empty;

            var viewModel = new StudentDashboardViewModel
            {
                StudentName = studentName,
                RoomNumber = "A-201",
                CurrentDate = DateTime.Now,
                CurfewTime = "10:00 PM",

                TodaysMenu = new List<MenuItem>
            {
                new MenuItem { MealType = "Breakfast", Description = "Paratha, Egg, Tea",         Time = "7:00 AM - 9:00 AM"  },
                new MenuItem { MealType = "Lunch",     Description = "Biryani, Raita, Salad",     Time = "12:00 PM - 2:00 PM" },
                new MenuItem { MealType = "Dinner",    Description = "Roti, Chicken Curry, Daal", Time = "7:00 PM - 9:00 PM"  }
            },

                Stats = new DashboardStats
                {
                    TotalAttendance = 95,
                    PendingComplaints = _complaintService.GetComplaintsByStudent(studentName)
                                                         .Count(c => c.Status == "Pending"),
                    OutstandingBills = 5000,
                    HostelBlock = "Block A"
                }
            };

            return View("~/Views/Student/Dashboard.cshtml", viewModel);
        }

        // STUDENT: View their complaints
        public IActionResult MyComplaints()
        {
            string studentName = User.Identity?.Name ?? string.Empty;
            var complaints = _complaintService.GetComplaintsByStudent(studentName);
            return View("~/Views/Student/MyComplaints.cshtml", complaints);
        }

        // STUDENT: View complaint details
        public IActionResult ComplaintDetails(int id)
        {
            var complaint = _complaintService.GetComplaintById(id);
            if (complaint == null)
                return NotFound();

            return View("~/Views/Student/ComplaintDetails.cshtml", complaint);
        }

        // STUDENT: Submit complaint (GET)
        [HttpGet]
        public IActionResult SubmitComplaint()
        {
            return View("~/Views/Student/SubmitComplaint.cshtml");
        }

        // STUDENT: Submit complaint (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitComplaint(Complaint complaint)
        {
            complaint.StudentName = User.Identity?.Name ?? string.Empty;
            _complaintService.AddComplaint(complaint);
            TempData["Success"] = "Complaint submitted successfully!";
            return RedirectToAction("MyComplaints");
        }
    }
}