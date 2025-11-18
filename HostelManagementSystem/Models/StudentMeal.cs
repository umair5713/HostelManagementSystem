namespace HostelManagementSystem.Models
{
    public class StudentMeal
    {
        public int StudentId { get; set; } // unique student
        public string Date { get; set; } = string.Empty;
        public string MealType { get; set; } = string.Empty; // Breakfast / Lunch / Dinner
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
