using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    public class StudentMealRepository : IStudentMealRepository
    {
        private readonly AppDbContext _context;

        public StudentMealRepository(AppDbContext context)
        {
            _context = context;
        }

        // ACCEPT MEAL
        public void AcceptMeal(StudentMeal meal)
        {
            if (!HasAccepted(meal.StudentID, meal.Date, meal.MealType))
            {
                // DEBUG — remove after fixing
                Console.WriteLine($"Inserting StudentID: {meal.StudentID}, Date: {meal.Date}, MealType: {meal.MealType}");

                _context.Database.ExecuteSqlRaw(
                    @"INSERT INTO tbl_student_meals (StudentID, Date, MealType, Time)
              VALUES ({0}, {1}, {2}, {3})",
                    meal.StudentID,
                    meal.Date,
                    meal.MealType,
                    DateTime.Now
                );
            }
        }

        // GET MEALS BY STUDENT
        public List<StudentMeal> GetMealsByStudent(int studentId)
        {
            return _context.StudentMeals
                      .FromSqlRaw("SELECT MealID, StudentID, Date, MealType, Time FROM tbl_student_meals WHERE StudentID = {0} ORDER BY Date DESC", studentId)
                      .ToList();
        }

        // HAS ACCEPTED
        public bool HasAccepted(int studentId, DateTime date, string mealType)
        {
            var result = _context.StudentMeals
                            .FromSqlRaw(@"SELECT MealID, StudentID, Date, MealType, Time 
                                      FROM tbl_student_meals 
                                      WHERE StudentID = {0} AND CAST(Date AS DATE) = CAST({1} AS DATE) AND MealType = {2}",
                                          studentId, date, mealType)
                            .FirstOrDefault();
            return result != null;
        }
    }
}
