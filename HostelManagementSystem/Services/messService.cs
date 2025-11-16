using System;
using System.Collections.Generic;
using System.Linq;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public class MessService : IMessService
    {
        // STUDENTS
        public List<Student> GetAllStudents()
        {
            return MessDataStore.GetAllStudents();
        }

        public Student GetStudentById(int studentId)
        {
            return MessDataStore.GetAllStudents().FirstOrDefault(s => s.StudentId == studentId);
        }

        // MENU
        public List<MenuItem> GetWeeklyMenu()
        {
            return MessDataStore.GetWeeklyMenu();
        }

        public List<MenuItem> GetMenuByDate(DateTime date)
        {
            return MessDataStore.GetWeeklyMenu()
                .Where(m => m.Date.Date == date.Date)
                .ToList();
        }

        // ATTENDANCE
        public List<MessAttendance> GetAttendanceByDate(DateTime date)
        {
            return MessDataStore.GetAttendanceByDate(date);
        }

        public List<MessAttendance> GetAttendanceByStudent(int studentId)
        {
            return MessDataStore.GetAttendanceByDate(DateTime.Today)
                .Where(a => a.StudentId == studentId)
                .ToList();
        }

        public void AddAttendance(MessAttendance attendance)
        {
            MessDataStore.AddAttendance(attendance);
        }

        // BILLING
        public List<MessBilling> GetAllBillings()
        {
            return MessDataStore.GetAllBillings();
        }

        public MessBilling GetBillingById(int billingId)
        {
            return MessDataStore.GetAllBillings()
                .FirstOrDefault(b => b.BillingId == billingId);
        }

        public void UpdateBilling(MessBilling billing)
        {
            MessDataStore.UpdateBilling(billing);
        }

        // DASHBOARD / VIEW MODELS
        public MessDashboardViewModel GetDashboardData()
        {
            var today = DateTime.Today;
            var todayAttendance = GetAttendanceByDate(today);

            return new MessDashboardViewModel
            {
                WeeklyMenu = GetWeeklyMenu(),
                TodayAttendance = todayAttendance,
                TotalStudents = GetAllStudents().Count,
                PresentToday = todayAttendance.Count(a => a.IsPresent),
                TodayRevenue = todayAttendance.Count(a => a.IsPresent) * 80 // assuming per meal charge
            };
        }

        public BillingViewModel GetBillingByMonthYear(int month, int year)
        {
            var billings = GetAllBillings()
                .Where(b => b.Month == month && b.Year == year)
                .ToList();

            return new BillingViewModel
            {
                BillingRecords = billings,
                SelectedMonth = month,
                SelectedYear = year,
                TotalRevenue = billings.Sum(b => b.GrandTotal)
            };
        }

        public AttendanceViewModel GetAttendanceViewModel(DateTime date, string mealType)
        {
            var students = GetAllStudents();
            var attendanceRecords = GetAttendanceByDate(date)
                .Where(a => a.MealType == mealType)
                .ToList();

            return new AttendanceViewModel
            {
                Students = students,
                SelectedDate = date,
                SelectedMealType = mealType,
                AttendanceRecords = attendanceRecords
            };
        }
    }
}

