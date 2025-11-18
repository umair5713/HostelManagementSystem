using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class StudentMenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IStudentMealService _mealService;

        
        private int StudentId => 1;

        public StudentMenuController(IMenuService menuService, IStudentMealService mealService)
        {
            _menuService = menuService;
            _mealService = mealService;
        }

        public IActionResult Index()
        {
            var menus = _menuService.GetMenus();
            return View(menus);
        }

        [HttpPost]
        public IActionResult AcceptMeal(string date, string mealType)
        {
            _mealService.AcceptMeal(StudentId, date, mealType);
            TempData["Message"] = $"{mealType} on {date} accepted!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AcceptAllMeals(string date)
        {
            // Accept all meals for the student
            _mealService.AcceptMeal(StudentId, date, "Breakfast");
            _mealService.AcceptMeal(StudentId, date, "Lunch");
            _mealService.AcceptMeal(StudentId, date, "Dinner");

            TempData["Message"] = $"All meals on {date} accepted!";
            return RedirectToAction("Index");
        }
        public IActionResult MealHistory()
        {
            var meals = _mealService.GetMealsByStudent(StudentId);
            return View(meals);
        }
    }
}
