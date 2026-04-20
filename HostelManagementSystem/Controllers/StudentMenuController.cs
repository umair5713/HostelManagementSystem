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

        // TODO: Replace with User.FindFirst("StudentID") when auth is ready
        private int GetStudentId() => 1;

        public IActionResult Index()
        {
            var menus = _menuService.GetMenus();
            return View(menus);
        }

        [HttpPost]
        public IActionResult AcceptMeal(DateTime date, string mealType)
        {
            int studentId = GetStudentId();
            _mealService.AcceptMeal(studentId, date, mealType);
            TempData["Message"] = $"{mealType} on {date:dd/MM/yyyy} accepted!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AcceptAllMeals(DateTime date)
        {
            int studentId = GetStudentId();
            _mealService.AcceptAllMeals(studentId, date);
            TempData["Message"] = $"All meals on {date:dd/MM/yyyy} accepted!";
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