using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagementSystem.Models
{

        public class Fee
        {
            [Key]
            public int FeeID { get; set; }

            [Required]
            public int StudentID { get; set; }

            [Required]
            [MaxLength(50)]
            public string Month { get; set; } = string.Empty; 

            [Required]
            [Range(1, 999999)]
            public decimal Amount { get; set; }

            public DateTime DueDate { get; set; }

            public DateTime? PaidDate { get; set; }            

            public bool IsPaid { get; set; } = false;

            [ForeignKey("StudentID")]
            public Student? Student { get; set; }            

            
            [NotMapped]
            public string StudentName => Student?.StudentName ?? string.Empty;
        }
    }

