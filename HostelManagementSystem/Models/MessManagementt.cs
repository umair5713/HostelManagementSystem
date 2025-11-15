using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HostelManagementSystem.Models
{
    // Student Model
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string RollNumber { get; set; }

        public string RoomNumber { get; set; }

        public string Course { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

    // Menu Item Model
    public class MenuItem
    {
        public int MenuId { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public string MealType { get; set; } // Breakfast, Lunch, Dinner

        [Required]
        public string Items { get; set; }

        public DateTime Date { get; set; }
    }

    // Attendance Model
    public class MessAttendance
    {
        public int AttendanceId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string RollNumber { get; set; }

        public DateTime Date { get; set; }

        public string MealType { get; set; }

        public bool IsPresent { get; set; }

        public TimeSpan CheckInTime { get; set; }
    }

    // Billing Model
    public class MessBilling
    {
        public int BillingId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string RollNumber { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int TotalDaysPresent { get; set; }

        public decimal PerMealCharge { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal MessMaintenanceFee { get; set; }

        public decimal GrandTotal { get; set; }

        public string Status { get; set; } // Paid, Unpaid, Pending

        public DateTime BillingDate { get; set; }
    }

    // View Models for Combined Data
    public class MessDashboardViewModel
    {
        public List<MenuItem> WeeklyMenu { get; set; }
        public List<MessAttendance> TodayAttendance { get; set; }
        public int TotalStudents { get; set; }
        public int PresentToday { get; set; }
        public decimal TodayRevenue { get; set; }
    }

    public class AttendanceViewModel
    {
        public List<Student> Students { get; set; }
        public DateTime SelectedDate { get; set; }
        public string SelectedMealType { get; set; }
        public List<MessAttendance> AttendanceRecords { get; set; }
    }

    public class BillingViewModel
    {
        public List<MessBilling> BillingRecords { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    // Static Data Store (Using Arrays and Lists as Data Structures)
    public static class MessDataStore
    {
        // Arrays for storing data
        private static Student[] students;
        private static MenuItem[] menuItems;
        private static MessAttendance[] attendanceRecords;
        private static MessBilling[] billingRecords;

        // Counters for array indexing
        private static int studentCount = 0;
        private static int menuCount = 0;
        private static int attendanceCount = 0;
        private static int billingCount = 0;

        // Array sizes
        private const int MAX_STUDENTS = 100;
        private const int MAX_MENU_ITEMS = 50;
        private const int MAX_ATTENDANCE = 1000;
        private const int MAX_BILLING = 500;

        static MessDataStore()
        {
            InitializeArrays();
            LoadDummyData();
        }

        private static void InitializeArrays()
        {
            students = new Student[MAX_STUDENTS];
            menuItems = new MenuItem[MAX_MENU_ITEMS];
            attendanceRecords = new MessAttendance[MAX_ATTENDANCE];
            billingRecords = new MessBilling[MAX_BILLING];
        }

        private static void LoadDummyData()
        {
            // Dummy Students
            string[] names = { "Ahmed Ali", "Fatima Khan", "Hassan Raza", "Ayesha Malik", "Bilal Ahmed",
                              "Zainab Hassan", "Usman Tariq", "Maryam Ashraf", "Ali Hamza", "Sara Noor" };
            string[] courses = { "Computer Science", "Software Engineering", "Information Technology", "Data Science" };

            for (int i = 0; i < 10; i++)
            {
                students[studentCount++] = new Student
                {
                    StudentId = i + 1,
                    Name = names[i],
                    RollNumber = $"20K-{1000 + i}",
                    RoomNumber = $"{(i % 3) + 1}{(i % 10) + 1:00}",
                    Course = courses[i % courses.Length],
                    Email = $"{names[i].Replace(" ", "").ToLower()}@university.edu.pk",
                    PhoneNumber = $"0300-{1234567 + i}"
                };
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
                // Breakfast
                menuItems[menuCount++] = new MenuItem
                {
                    MenuId = (i * 3) + 1,
                    Day = days[i],
                    MealType = "Breakfast",
                    Items = breakfasts[i],
                    Date = today.AddDays(i - (int)today.DayOfWeek + 1)
                };

                // Lunch
                menuItems[menuCount++] = new MenuItem
                {
                    MenuId = (i * 3) + 2,
                    Day = days[i],
                    MealType = "Lunch",
                    Items = lunches[i],
                    Date = today.AddDays(i - (int)today.DayOfWeek + 1)
                };

                // Dinner
                menuItems[menuCount++] = new MenuItem
                {
                    MenuId = (i * 3) + 3,
                    Day = days[i],
                    MealType = "Dinner",
                    Items = dinners[i],
                    Date = today.AddDays(i - (int)today.DayOfWeek + 1)
                };
            }

            // Dummy Attendance for last 7 days
            Random rand = new Random();
            for (int day = 0; day < 7; day++)
            {
                DateTime attendanceDate = DateTime.Today.AddDays(-day);
                string[] mealTypes = { "Breakfast", "Lunch", "Dinner" };

                foreach (string mealType in mealTypes)
                {
                    for (int i = 0; i < studentCount; i++)
                    {
                        bool isPresent = rand.Next(100) > 20; // 80% attendance
                        attendanceRecords[attendanceCount++] = new MessAttendance
                        {
                            AttendanceId = attendanceCount + 1,
                            StudentId = students[i].StudentId,
                            StudentName = students[i].Name,
                            RollNumber = students[i].RollNumber,
                            Date = attendanceDate,
                            MealType = mealType,
                            IsPresent = isPresent,
                            CheckInTime = isPresent ? new TimeSpan(rand.Next(7, 10), rand.Next(0, 60), 0) : TimeSpan.Zero
                        };
                    }
                }
            }

            // Dummy Billing Records
            for (int i = 0; i < studentCount; i++)
            {
                int daysPresent = rand.Next(70, 90);
                decimal perMealCharge = 80m;
                decimal maintenanceFee = 500m;

                billingRecords[billingCount++] = new MessBilling
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
                };
            }
        }

        // Methods to access data
        public static List<Student> GetAllStudents()
        {
            List<Student> list = new List<Student>();
            for (int i = 0; i < studentCount; i++)
            {
                if (students[i] != null)
                    list.Add(students[i]);
            }
            return list;
        }

        public static List<MenuItem> GetWeeklyMenu()
        {
            List<MenuItem> list = new List<MenuItem>();
            for (int i = 0; i < menuCount; i++)
            {
                if (menuItems[i] != null)
                    list.Add(menuItems[i]);
            }
            return list;
        }

        public static List<MessAttendance> GetAttendanceByDate(DateTime date)
        {
            List<MessAttendance> list = new List<MessAttendance>();
            for (int i = 0; i < attendanceCount; i++)
            {
                if (attendanceRecords[i] != null && attendanceRecords[i].Date.Date == date.Date)
                    list.Add(attendanceRecords[i]);
            }
            return list;
        }

        public static List<MessBilling> GetAllBillings()
        {
            List<MessBilling> list = new List<MessBilling>();
            for (int i = 0; i < billingCount; i++)
            {
                if (billingRecords[i] != null)
                    list.Add(billingRecords[i]);
            }
            return list;
        }

        public static void AddAttendance(MessAttendance attendance)
        {
            if (attendanceCount < MAX_ATTENDANCE)
            {
                attendance.AttendanceId = attendanceCount + 1;
                attendanceRecords[attendanceCount++] = attendance;
            }
        }

        public static void UpdateBilling(MessBilling billing)
        {
            for (int i = 0; i < billingCount; i++)
            {
                if (billingRecords[i] != null && billingRecords[i].BillingId == billing.BillingId)
                {
                    billingRecords[i] = billing;
                    break;
                }
            }
        }
    }
}