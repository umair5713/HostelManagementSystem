using HostelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class BillingController : Controller
    {
        private readonly IBillingService _service;

        public BillingController(IBillingService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var bills = _service.GetAllBills();
            return View(bills);
        }

        public IActionResult Details(int studentId)
        {
            var bill = _service.GetBill(studentId);
            if (bill == null) return NotFound();
            return View(bill);
        }

        [HttpPost]
        public IActionResult Generate(int studentId, int mealCount, int rate)
        {
            _service.GenerateBill(studentId, mealCount, rate);
            return RedirectToAction("Details", new { studentId });
        }

        [HttpPost]
        public IActionResult Pay(int studentId)
        {
            _service.MarkAsPaid(studentId);
            return RedirectToAction("Details", new { studentId });
        }
    }
}
