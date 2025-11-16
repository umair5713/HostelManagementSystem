using Microsoft.AspNetCore.Mvc;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Controllers
{
    public class MessController : Controller
    {
        // Dashboard - Main Landing Page
        public IActionResult Index()
        {
            var viewModel = new MessDashboardViewModel
            {
                WeeklyMenu = MessDataStore.GetWeeklyMenu(),
                TodayAttendance = MessDataStore.GetAttendanceByDate(DateTime.Today),
                TotalStudents = MessDataStore.GetAllStudents().Count,
                PresentToday = MessDataStore.GetAttendanceByDate(DateTime.Today)
                    .Count(a => a.IsPresent),
                TodayRevenue = MessDataStore.GetAttendanceByDate(DateTime.Today)
                    .Count(a => a.IsPresent) * 80m
            };

            return View(viewModel);
        }

        // Weekly Menu View
        public IActionResult WeeklyMenu()
        {
            var menuItems = MessDataStore.GetWeeklyMenu();
            return View(menuItems);
        }

        // Attendance Management - View
        public IActionResult Attendance(DateTime? date, string? mealType)
        {
            DateTime selectedDate = date ?? DateTime.Today;
            string selectedMeal = mealType ?? "Lunch";

            var students = MessDataStore.GetAllStudents();
            var attendanceRecords = MessDataStore.GetAttendanceByDate(selectedDate)
                .Where(a => a.MealType == selectedMeal)
                .ToList();

            var viewModel = new AttendanceViewModel
            {
                Students = students,
                SelectedDate = selectedDate,
                SelectedMealType = selectedMeal,
                AttendanceRecords = attendanceRecords
            };

            return View(viewModel);
        }

        // Mark Attendance - POST
        [HttpPost]
        public IActionResult MarkAttendance(int studentId, DateTime date, string mealType, bool isPresent)
        {
            try
            {
                var student = MessDataStore.GetAllStudents()
                    .FirstOrDefault(s => s.StudentId == studentId);

                if (student != null)
                {
                    var attendance = new MessAttendance
                    {
                        StudentId = studentId,
                        StudentName = student.Name,
                        RollNumber = student.RollNumber,
                        Date = date,
                        MealType = mealType,
                        IsPresent = isPresent,
                        CheckInTime = isPresent ? DateTime.Now.TimeOfDay : TimeSpan.Zero
                    };

                    MessDataStore.AddAttendance(attendance);

                    return Json(new { success = true, message = "Attendance marked successfully!" });
                }

                return Json(new { success = false, message = "Student not found!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Billing Management - View
        public IActionResult Billing(int? month, int? year)
        {
            int selectedMonth = month ?? DateTime.Now.Month;
            int selectedYear = year ?? DateTime.Now.Year;

            var billings = MessDataStore.GetAllBillings()
                .Where(b => b.Month == selectedMonth && b.Year == selectedYear)
                .ToList();

            var viewModel = new BillingViewModel
            {
                BillingRecords = billings,
                SelectedMonth = selectedMonth,
                SelectedYear = selectedYear,
                TotalRevenue = billings.Sum(b => b.GrandTotal)
            };

            return View(viewModel);
        }

        // Generate Bill - POST
        [HttpPost]
        public IActionResult GenerateBill(int studentId, int month, int year)
        {
            try
            {
                var student = MessDataStore.GetAllStudents()
                    .FirstOrDefault(s => s.StudentId == studentId);

                if (student == null)
                {
                    return Json(new { success = false, message = "Student not found!" });
                }

                // Calculate attendance for the month
                DateTime startDate = new DateTime(year, month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                int totalDaysPresent = 0;
                DateTime currentDate = startDate;

                while (currentDate <= endDate)
                {
                    var dayAttendance = MessDataStore.GetAttendanceByDate(currentDate)
                        .Where(a => a.StudentId == studentId && a.IsPresent)
                        .ToList();

                    totalDaysPresent += dayAttendance.Count;
                    currentDate = currentDate.AddDays(1);
                }

                decimal perMealCharge = 80m;
                decimal maintenanceFee = 500m;
                decimal totalAmount = totalDaysPresent * perMealCharge;
                decimal grandTotal = totalAmount + maintenanceFee;

                var billing = new MessBilling
                {
                    StudentId = studentId,
                    StudentName = student.Name,
                    RollNumber = student.RollNumber,
                    Month = month,
                    Year = year,
                    TotalDaysPresent = totalDaysPresent,
                    PerMealCharge = perMealCharge,
                    TotalAmount = totalAmount,
                    MessMaintenanceFee = maintenanceFee,
                    GrandTotal = grandTotal,
                    Status = "Unpaid",
                    BillingDate = DateTime.Now
                };

                MessDataStore.UpdateBilling(billing);

                return Json(new
                {
                    success = true,
                    message = "Bill generated successfully!",
                    billData = billing
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Update Payment Status - POST
        [HttpPost]
        public IActionResult UpdatePaymentStatus(int billingId, string status)
        {
            try
            {
                var billing = MessDataStore.GetAllBillings()
                    .FirstOrDefault(b => b.BillingId == billingId);

                if (billing != null)
                {
                    billing.Status = status;
                    MessDataStore.UpdateBilling(billing);

                    return Json(new { success = true, message = "Payment status updated successfully!" });
                }

                return Json(new { success = false, message = "Billing record not found!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Search Students - AJAX
        [HttpGet]
        public IActionResult SearchStudents(string searchTerm)
        {
            var students = MessDataStore.GetAllStudents()
                .Where(s => s.Name.ToLower().Contains(searchTerm.ToLower()) ||
                           s.RollNumber.ToLower().Contains(searchTerm.ToLower()))
                .Select(s => new
                {
                    s.StudentId,
                    s.Name,
                    s.RollNumber,
                    s.RoomNumber
                })
                .ToList();

            return Json(students);
        }

        // Get Attendance Report - AJAX
        [HttpGet]
        public IActionResult GetAttendanceReport(int studentId, DateTime startDate, DateTime endDate)
        {
            var allAttendance = new List<MessAttendance>();
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                var dayAttendance = MessDataStore.GetAttendanceByDate(currentDate)
                    .Where(a => a.StudentId == studentId)
                    .ToList();

                allAttendance.AddRange(dayAttendance);
                currentDate = currentDate.AddDays(1);
            }

            var report = new
            {
                TotalDays = (endDate - startDate).Days + 1,
                PresentDays = allAttendance.Count(a => a.IsPresent),
                AbsentDays = allAttendance.Count(a => !a.IsPresent),
                AttendancePercentage = allAttendance.Count > 0 ?
                    (allAttendance.Count(a => a.IsPresent) * 100.0 / allAttendance.Count) : 0,
                Records = allAttendance.Select(a => new
                {
                    Date = a.Date.ToString("dd-MMM-yyyy"),
                    a.MealType,
                    Status = a.IsPresent ? "Present" : "Absent",
                    CheckInTime = a.IsPresent ? a.CheckInTime.ToString(@"hh\:mm") : "-"
                })
            };

            return Json(report);
        }
    }
}