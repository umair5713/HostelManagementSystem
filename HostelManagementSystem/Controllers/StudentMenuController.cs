//using HostelManagementSystem.Models;
//using HostelManagementSystem.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace HostelManagementSystem.Controllers
//{
//    public class StudentMenuController : Controller
//    {
//        private readonly IMenuService _menuService;
//        private readonly IStudentMealService _mealService;

//        public StudentMenuController(IMenuService menuService, IStudentMealService mealService)
//        {
//            _menuService = menuService;
//            _mealService = mealService;
//        }

//        // TODO: Replace with User.FindFirst("StudentID") when auth is ready
//        private int GetStudentId() => 1;

//        public IActionResult Index()
//        {
//            var menus = _menuService.GetMenus();
//            return View(menus);
//        }

//        [HttpPost]
//        public IActionResult AcceptMeal(DateTime date, string mealType)
//        {
//            int studentId = GetStudentId();
//            _mealService.AcceptMeal(studentId, date, mealType);
//            TempData["Message"] = $"{mealType} on {date:dd/MM/yyyy} accepted!";
//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public IActionResult AcceptAllMeals(DateTime date)
//        {
//            int studentId = GetStudentId();
//            _mealService.AcceptAllMeals(studentId, date);
//            TempData["Message"] = $"All meals on {date:dd/MM/yyyy} accepted!";
//            return RedirectToAction("Index");
//        }

//        public IActionResult MealHistory()
//        {
//            int studentId = GetStudentId();
//            var meals = _mealService.GetMealsByStudent(studentId);
//            return View(meals);
//        }
//    }
//}


using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class StudentMenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IStudentMealService _mealService;

        public StudentMenuController(IMenuService menuService, IStudentMealService mealService)
        {
            _menuService = menuService;
            _mealService = mealService;
        }

        private int GetStudentId() =>
            HttpContext.Session.GetInt32("StudentID") ?? 0;

        // Check if current time is within meal acceptance window
        private bool IsWithinWindow(string mealType)
        {
            var now = DateTime.Now.TimeOfDay;
            return mealType switch
            {
                "Breakfast" => now >= new TimeSpan(7, 0, 0) && now <= new TimeSpan(9, 0, 0),
                "Lunch" => now >= new TimeSpan(12, 0, 0) && now <= new TimeSpan(14, 0, 0),
                "Dinner" => now >= new TimeSpan(19, 0, 0) && now <= new TimeSpan(21, 0, 0),
                _ => false
            };
        }

        public IActionResult Index()
        {
            int studentId = GetStudentId();
            var menus = _menuService.GetMenus();
            var now = DateTime.Now;

            // Pass time window info to view
            ViewBag.BreakfastOpen = IsWithinWindow("Breakfast");
            ViewBag.LunchOpen = IsWithinWindow("Lunch");
            ViewBag.DinnerOpen = IsWithinWindow("Dinner");

            // Check what student has already accepted/declined today
            ViewBag.BreakfastStatus = _mealService.HasAccepted(studentId, now.Date, "Breakfast")
                ? "Accepted"
                : _mealService.HasAccepted(studentId, now.Date, "Breakfast_Declined")
                    ? "Declined" : "Pending";

            ViewBag.LunchStatus = _mealService.HasAccepted(studentId, now.Date, "Lunch")
                ? "Accepted"
                : _mealService.HasAccepted(studentId, now.Date, "Lunch_Declined")
                    ? "Declined" : "Pending";

            ViewBag.DinnerStatus = _mealService.HasAccepted(studentId, now.Date, "Dinner")
                ? "Accepted"
                : _mealService.HasDeclined(studentId, now.Date, "Dinner_Declined")
                    ? "Declined" : "Pending";

            // Pass window end times for countdown timer
            ViewBag.BreakfastEnd = "09:00:00";
            ViewBag.LunchEnd = "14:00:00";
            ViewBag.DinnerEnd = "21:00:00";

            return View(menus);
        }

        [HttpPost]
        public IActionResult AcceptMeal(DateTime date, string mealType)
        {
            int studentId = GetStudentId();
            if (!IsWithinWindow(mealType))
            {
                TempData["Message"] = $"Acceptance window for {mealType} has closed.";
                return RedirectToAction("Index");
            }
            _mealService.AcceptMeal(studentId, date, mealType);
            TempData["Message"] = $"{mealType} accepted!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AcceptAllMeals(DateTime date)
        {
            int studentId = GetStudentId();
            _mealService.AcceptAllMeals(studentId, date);
            TempData["Message"] = "All available meals accepted!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeclineMeal(DateTime date, string mealType)
        {
            int studentId = GetStudentId();
            _mealService.DeclineMeal(studentId, date, mealType);
            TempData["Message"] = $"{mealType} declined.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeclineAllMeals(DateTime date)
        {
            int studentId = GetStudentId();
            _mealService.DeclineAllMeals(studentId, date);
            TempData["Message"] = "All meals declined.";
            return RedirectToAction("Index");
        }

        public IActionResult MealHistory()
        {
            int studentId = GetStudentId();
            var meals = _mealService.GetMealsByStudent(studentId);
            return View(meals);
        }
    }
}