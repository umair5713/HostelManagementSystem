using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagementSystem.Models
{
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }

        [Required]
        public DateTime Date { get; set; } 

        [MaxLength(200)]
        public string Breakfast { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Lunch { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Dinner { get; set; } = string.Empty;
    }
}
