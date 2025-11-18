using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
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

        // ✅ ADD THIS NEW METHOD
        public IActionResult Dashboard()
        {
            // For now, using hardcoded data
            // Later you can fetch from database using student ID from session

            var viewModel = new StudentDashboardViewModel
            {
                StudentName = "Ahmad Ali", // Replace with logged-in student
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
    }
}