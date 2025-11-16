 using HostelManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace HostelManagementSystem.Repositories
    {
        // Interface for Mess Repository
        public interface IMessRepository
        {
            List<Student> GetAllStudents();
            List<MenuItem> GetWeeklyMenu();
            List<MessAttendance> GetAttendanceByDate(DateTime date);
            List<MessBilling> GetAllBillings();
            void AddAttendance(MessAttendance attendance);
            void UpdateBilling(MessBilling billing);
        }
    }

}
