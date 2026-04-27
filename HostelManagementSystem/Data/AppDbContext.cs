using Microsoft.EntityFrameworkCore;
using HostelManagementSystem.Models;
namespace HostelManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMeal> StudentMeals { get; set; }
        public DbSet<Menu> Menus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>().ToTable("tbl_complaints");
            modelBuilder.Entity<AttendanceRecord>().ToTable("tbl_attendance");
            modelBuilder.Entity<Student>().ToTable("tbl_students");
            modelBuilder.Entity<StudentMeal>().ToTable("tbl_student_meals");
            modelBuilder.Entity<Menu>().ToTable("tbl_menu");
            modelBuilder.Entity<User>().ToTable("tbl_user");
            modelBuilder.Entity<Role>().ToTable("tbl_role");
        }

    }
}
