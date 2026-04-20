using System.ComponentModel.DataAnnotations;

namespace HostelManagementSystem.Models
{
    public class StudentMeal
    {
        [Key]
        public int MealID { get; set; }
        public int StudentID { get; set; } 
        public DateTime Date { get; set; }

        [MaxLength(50)]
        public string MealType { get; set; } = string.Empty; // Breakfast / Lunch / Dinner
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
