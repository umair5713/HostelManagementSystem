using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Services;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class FeeController : Controller
    {
        private readonly IFeeService _service;
        private readonly IStudentService _studentService;

        public FeeController(IFeeService service, IStudentService studentService)
        {
            _service = service;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            var fees = _service.GetAllFees();
            return View(fees);
        }

        // CREATE (GET)
        public IActionResult Create()
        {

            ViewBag.Students = _studentService.GetAllStudents();
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fee fee)
        {
            Console.WriteLine($"StudentID: {fee.StudentID}, Month: {fee.Month}, Amount: {fee.Amount}");

            _service.AddFee(fee);
            return RedirectToAction("Index");
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var fee = _service.GetById(id);  // ✅ use GetById not GetAllFees
            if (fee == null) return NotFound();

            ViewBag.Students = _studentService.GetAllStudents();
            return View(fee);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Fee fee)
        {
            _service.EditFee(fee);
            return RedirectToAction("Index");
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var fee = _service.GetById(id);  // ✅ use GetById not GetAllFees
            if (fee == null) return NotFound();

            return View(fee);
        }

        // DELETE (POST) ✅ must be POST not GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _service.DeleteFee(id);
            return RedirectToAction("Index");
        }
    }
}