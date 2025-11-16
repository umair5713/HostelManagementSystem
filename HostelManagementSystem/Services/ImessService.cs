using System;
using System.Collections.Generic;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IMessService
    {
        // Students
        List<Student> GetAllStudents();
        Student GetStudentById(int studentId);

        // Menu
        List<MenuItem> GetWeeklyMenu();
        List<MenuItem> GetMenuByDate(DateTime date);

        // Attendance
        List<MessAttendance> GetAttendanceByDate(DateTime date);
        List<MessAttendance> GetAttendanceByStudent(int studentId);
        void AddAttendance(MessAttendance attendance);

        // Billing
        List<MessBilling> GetAllBillings();
        MessBilling GetBillingById(int billingId);
        void UpdateBilling(MessBilling billing);

        // Dashboard / Reports
        MessDashboardViewModel GetDashboardData();
        BillingViewModel GetBillingByMonthYear(int month, int year);
        AttendanceViewModel GetAttendanceViewModel(DateTime date, string mealType);
    }
}
