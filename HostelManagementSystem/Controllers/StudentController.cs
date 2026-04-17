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
        public IActionResult Register(Student student)
        {
            _service.RegisterStudent(student);
            return RedirectToAction("Index");
        }

        public IActionResult SortByID()
        {
            var sorted = _service.SortByID();
            return View("~/Views/Student/Index.cshtml", sorted);
        }

        public IActionResult Dashboard()
        {
            var viewModel = new StudentDashboardViewModel
            {
                StudentName = "Ahmad Ali",
                RoomNumber = "A-201",
                CurrentDate = DateTime.Now,
                CurfewTime = "10:00 PM",

                TodaysMenu = new List<MenuItem>
                {
                    new MenuItem
                    {
                        MealType = "Breakfast",
                        Description = "Paratha, Egg, Tea",
                        Time = "7:00 AM - 9:00 AM"
                    },
                    new MenuItem
                    {
                        MealType = "Lunch",
                        Description = "Biryani, Raita, Salad",
                        Time = "12:00 PM - 2:00 PM"
                    },
                    new MenuItem
                    {
                        MealType = "Dinner",
                        Description = "Roti, Chicken Curry, Daal",
                        Time = "7:00 PM - 9:00 PM"
                    }
                },

                Stats = new DashboardStats
                {
                    TotalAttendance = 95,
                    PendingComplaints = 2,
                    OutstandingBills = 5000,
                    HostelBlock = "Block A"
                }
            };

            return View("~/Views/Student/Dashboard.cshtml", viewModel);
        }

        // STUDENT: View their complaints
        public IActionResult MyComplaints()
        {
            // TODO: Get student name from session/auth
            string studentName = "Ahmad Ali"; // Replace with actual logged-in student

            var complaints = _complaintService.GetComplaintsByStudent(studentName);
            return View("~/Views/Student/MyComplaints.cshtml", complaints);
        }

        // STUDENT: Submit complaint form
        [HttpGet]
        public IActionResult SubmitComplaint()
        {
            return View("~/Views/Student/SubmitComplaint.cshtml");
        }

        //STUDENT: Submit complaint action
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