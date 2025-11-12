using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class MessController : Controller
    {
        // Dummy data stored in an array
        private MessManagementt[] messRecords;

        public MessController()
        {
            // Initialize with some fake data
            messRecords = new MessManagementt[]
            {
                new MessManagementt { StudentID = 1, StudentName = "Ali", MealsTaken = 20, MealRate = 150 },
                new MessManagementt { StudentID = 2, StudentName = "Ahmed", MealsTaken = 15, MealRate = 150 },
                new MessManagementt { StudentID = 3, StudentName = "Sara", MealsTaken = 18, MealRate = 150 },
                new MessManagementt { StudentID = 4, StudentName = "Zainab", MealsTaken = 22, MealRate = 150 }
            };
        }

        // Action to show all mess records
        public IActionResult Index()
        {
            return View(messRecords);
        }

        // Action to get details for one student by ID
        public IActionResult Details(int id)
        {
            var record = messRecords.FirstOrDefault(m => m.StudentID == id);
            if (record == null)
                return NotFound();

            return View(record);
        }
    }
}
