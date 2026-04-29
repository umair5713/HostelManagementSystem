//using HostelManagementSystem.Models;
//using HostelManagementSystem.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace HostelManagementSystem.Controllers
//{
//    public class ComplaintsController : Controller
//    {
//        private readonly IComplaintService _service;

//        public ComplaintsController(IComplaintService service)
//        {
//            _service = service;
//        }


//        public IActionResult Index()
//        {
//            var complaints = _service.GetAllComplaints();
//            return View("~/Views/Complaints/Index.cshtml", complaints);
//        }

//        public IActionResult Admin()
//        {
//            var complaints = _service.GetAllComplaints();
//            return View("~/Views/Complaints/Admin.cshtml", complaints);
//        }

//        // ADMIN: Update status to "Received"
//        [HttpPost]
//        public IActionResult MarkReceived(int id)
//        {
//            _service.UpdateComplaintStatus(id, "Received");
//            return RedirectToAction("Admin");
//        }

//        // ADMIN: Update status to "In Progress"
//        [HttpPost]
//        public IActionResult MarkInProgress(int id)
//        {
//            _service.UpdateComplaintStatus(id, "In Progress");
//            return RedirectToAction("Admin");
//        }

//        // ADMIN: Update status to "Done"
//        [HttpPost]
//        public IActionResult MarkDone(int id)
//        {
//            _service.UpdateComplaintStatus(id, "Done");
//            return RedirectToAction("Admin");
//        }

//        // ADMIN: Delete complaint
//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            _service.DeleteComplaint(id);
//            return RedirectToAction("Admin");
//        }

//        // ADMIN: View complaint details
//        public IActionResult Details(int id)
//        {
//            var complaint = _service.GetComplaintById(id);
//            if (complaint == null)
//                return NotFound();

//            return View("~/Views/Complaints/Details.cshtml", complaint);
//        }

//        // STUDENT: View submit form (GET)
//        [HttpGet]
//        public IActionResult Submit()
//        {
//            return View("~/Views/Complaints/Submit.cshtml");
//        }

//        // STUDENT: Submit complaint (POST)
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Submit(Complaint complaint)
//        {
//            _service.AddComplaint(complaint); // Time & Status set inside service
//            TempData["Success"] = "Complaint submitted successfully!";
//            return RedirectToAction("Index");
//        }
//    }
//}
using HostelManagementSystem.Models;
using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly IComplaintService _service;

        public ComplaintsController(IComplaintService service)
        {
            _service = service;
        }

        // 🔹 STUDENT: View ONLY their complaints
        public IActionResult Index()
        {
            string studentName = HttpContext.Session.GetString("Username") ?? string.Empty;

            var complaints = _service.GetComplaintsByStudent(studentName);

            return View("~/Views/Complaints/Index.cshtml", complaints);
        }

        // 🔹 ADMIN: View ALL complaints
        public IActionResult Admin()
        {
            var complaints = _service.GetAllComplaints();
            return View("~/Views/Complaints/Admin.cshtml", complaints);
        }

        // 🔹 ADMIN: Update status
        [HttpPost]
        public IActionResult MarkReceived(int id)
        {
            _service.UpdateComplaintStatus(id, "Received");
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult MarkInProgress(int id)
        {
            _service.UpdateComplaintStatus(id, "In Progress");
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult MarkDone(int id)
        {
            _service.UpdateComplaintStatus(id, "Done");
            return RedirectToAction("Admin");
        }

        // 🔹 ADMIN: Delete complaint
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.DeleteComplaint(id);
            return RedirectToAction("Admin");
        }

        // 🔹 ADMIN: View complaint details
        public IActionResult Details(int id)
        {
            var complaint = _service.GetComplaintById(id);
            if (complaint == null)
                return NotFound();

            return View("~/Views/Complaints/Details.cshtml", complaint);
        }

        // 🔹 STUDENT: Show submit form
        [HttpGet]
        public IActionResult Submit()
        {
            return View("~/Views/Complaints/Submit.cshtml");
        }

        // 🔹 STUDENT: Submit complaint
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Complaint complaint)
        {
            string studentName = HttpContext.Session.GetString("Username") ?? string.Empty;

            complaint.StudentName = studentName;

            _service.AddComplaint(complaint);

            TempData["Success"] = "Complaint submitted successfully!";
            return RedirectToAction("Index");
        }
    }
}