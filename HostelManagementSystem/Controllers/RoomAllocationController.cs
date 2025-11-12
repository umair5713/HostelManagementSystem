using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class RoomAllocationController : Controller
    {
        // One shared queue for the whole system
        public static RoomQueue queue = new RoomQueue();

        public IActionResult QueueList()
        {
            ViewBag.AllStudents = StudentController.students.GetStudents();
            var waitingStudents = queue.get_queue();
            return View("~/Views/RoomAllocation/QueueList.cshtml", waitingStudents);
        }

        [HttpPost]
        public IActionResult AddToQueue(Student student)
        {
            queue.enqueue(student);
            return RedirectToAction("QueueList");
        }

        public IActionResult AllocateRoom()
        {
            var student = queue.dequeue();

            // If queue was empty
            if (student == null)
            {
                return View("Allocated", null);
            }

            // Show allocated student result page
            return View("Allocated", student);
        }

    }
}

