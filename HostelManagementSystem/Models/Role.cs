using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagementSystem.Models
{
    [Table("tbl_role")]
    public class Role
    {
        [Column("role_id")]
        public int Id { get; set; }
        [Column("role_name")]
        public string RoleName { get; set; } = string.Empty;
    }
}
