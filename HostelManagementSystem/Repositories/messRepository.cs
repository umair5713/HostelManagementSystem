using HostelManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace HostelManagementSystem.Repositories
{
    public class MessRepository : IMessRepository
    {
        private readonly List<Student> students = new();
        private readonly List<MenuItem> menuItems = new();
        private readonly List<MessAttendance> attendanceRecords = new();
        private readonly List<MessBilling> billingRecords = new();

        public MessRepository()
        {
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // Dummy Students
            string[] names = { "Ahmed Ali", "Fatima Khan", "Hassan Raza", "Ayesha Malik", "Bilal Ahmed",
                              "Zainab Hassan", "Usman Tariq", "Maryam Ashraf", "Ali Hamza", "Sara Noor" };
            string[] courses = { "Computer Science", "Software Engineering", "Information Technology", "Data Science" };

            for (int i = 0; i < 10; i++)
            {
                students.Add(new Student
                {
                    StudentId = i + 1,
                    Name = names[i],
                    RollNumber = $"20K-{1000 + i}",
                    RoomNumber = $"{(i % 3) + 1}{(i % 10) + 1:00}",
                    Course = courses[i % courses.Length],
                    Email = $"{names[i].Replace(" ", "").ToLower()}@university.edu.pk",
                    PhoneNumber = $"0300-{1234567 + i}"
                });
            }

            // Dummy Weekly Menu
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string[] breakfasts = { "Paratha, Omelette, Tea", "Halwa Puri, Channay", "Bread, Jam, Butter, Tea",
                                   "Daal Chawal, Tea", "French Toast, Tea", "Aloo Paratha, Yogurt", "Pancakes, Tea" };
            string[] lunches = { "Chicken Biryani, Raita", "Daal, Rice, Roti", "Qorma, Naan, Rice",
                                "Karahi, Roti, Salad", "Palak Paneer, Rice, Roti", "Nihari, Naan", "Mix Vegetables, Rice, Roti" };
            string[] dinners = { "Fried Rice, Chicken", "Pulao, Raita", "Pasta, Garlic Bread",
                                "Biryani, Raita", "Karahi, Roti", "Chinese Rice, Manchurian", "Chicken Tikka, Naan" };

            DateTime today = DateTime.Today;
            for (int i = 0; i < 7; i++)
            {
                menuItems.Add(new MenuItem { MenuId = (i * 3) + 1, Day = days[i], MealType = "Breakfast", Items = breakfasts[i], Date = today.AddDays(i - (int)today.DayOfWeek + 1) });
                menuItems.Add(new MenuItem { MenuId = (i * 3) + 2, Day = days[i], MealType = "Lunch", Items = lunches[i], Date = today.AddDays(i - (int)today.DayOfWeek + 1) });
                menuItems.Add(new MenuItem { MenuId = (i * 3) + 3, Day = days[i], MealType = "Dinner", Items = dinners[i], Date = today.AddDays(i - (int)today.DayOfWeek + 1) });
            }

            // Dummy Attendance for last 7 days
            Random rand = new Random();
            for (int day = 0; day < 7; day++)
            {
                DateTime attendanceDate = DateTime.Today.AddDays(-day);
                string[] mealTypes = { "Breakfast", "Lunch", "Dinner" };

                foreach (string mealType in mealTypes)
                {
                    foreach (var student in students)
                    {
                        bool isPresent = rand.Next(100) > 20; // 80% attendance
                        attendanceRecords.Add(new MessAttendance
                        {
                            AttendanceId = attendanceRecords.Count + 1,
                            StudentId = student.StudentId,
                            StudentName = student.Name,
                            RollNumber = student.RollNumber,
                            Date = attendanceDate,
                            MealType = mealType,
                            IsPresent = isPresent,
                            CheckInTime = isPresent ? new TimeSpan(rand.Next(7, 10), rand.Next(0, 60), 0) : TimeSpan.Zero
                        });
                    }
                }
            }

            // Dummy Billing Records
            for (int i = 0; i < students.Count; i++)
            {
                int daysPresent = rand.Next(70, 90);
                decimal perMealCharge = 80m;
                decimal maintenanceFee = 500m;

                billingRecords.Add(new MessBilling
                {
                    BillingId = i + 1,
                    StudentId = students[i].StudentId,
                    StudentName = students[i].Name,
                    RollNumber = students[i].RollNumber,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    TotalDaysPresent = daysPresent,
                    PerMealCharge = perMealCharge,
                    TotalAmount = daysPresent * perMealCharge,
                    MessMaintenanceFee = maintenanceFee,
                    GrandTotal = (daysPresent * perMealCharge) + maintenanceFee,
                    Status = i % 3 == 0 ? "Paid" : (i % 3 == 1 ? "Pending" : "Unpaid"),
                    BillingDate = DateTime.Now.AddDays(-rand.Next(1, 30))
                });
            }
        }

        // Implementation of IMessRepository
        public List<Student> GetAllStudents() => students;

        public List<MenuItem> GetWeeklyMenu() => menuItems;

        public List<MessAttendance> GetAttendanceByDate(DateTime date) =>
            attendanceRecords.FindAll(a => a.Date.Date == date.Date);

        public List<MessBilling> GetAllBillings() => billingRecords;

        public void AddAttendance(MessAttendance attendance)
        {
            attendance.AttendanceId = attendanceRecords.Count + 1;
            attendanceRecords.Add(attendance);
        }

        public void UpdateBilling(MessBilling billing)
        {
            var index = billingRecords.FindIndex(b => b.BillingId == billing.BillingId);
            if (index >= 0)
                billingRecords[index] = billing;
        }
    }
}
