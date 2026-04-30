using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class StudentMealService : IStudentMealService
    {
        private readonly IStudentMealRepository _repo;

        public StudentMealService(IStudentMealRepository repo)
        {
            _repo = repo;
        }

        public void AcceptMeal(int studentId, DateTime date, string mealType)
        {
            var meal = new StudentMeal
            {
                StudentID = studentId,
                Date = date,
                MealType = mealType,
                Time = DateTime.Now
            };
            _repo.AcceptMeal(meal);
        }

        public void AcceptAllMeals(int studentId, DateTime date)
        {
            AcceptMeal(studentId, date, "Breakfast");
            AcceptMeal(studentId, date, "Lunch");
            AcceptMeal(studentId, date, "Dinner");
        }

        public List<StudentMeal> GetMealsByStudent(int studentId)
        {
            return _repo.GetMealsByStudent(studentId);
        }

        public bool HasAccepted(int studentId, DateTime date, string mealType)
        {
            return _repo.HasAccepted(studentId, date, mealType);
        }

        public void DeclineMeal(int studentId, DateTime date, string mealType)
        {
            var meal = new StudentMeal
            {
                StudentID = studentId,
                Date = date,
                MealType = mealType,
                Status = "Declined",
                Time = DateTime.Now
            };
            _repo.AcceptMeal(meal);
        }

        public void DeclineAllMeals(int studentId, DateTime date)
        {
            DeclineMeal(studentId, date, "Breakfast");
            DeclineMeal(studentId, date, "Lunch");
            DeclineMeal(studentId, date, "Dinner");
        }

        public bool HasDeclined(int studentId, DateTime date, string mealType)
        {
            return _repo.HasDeclined(studentId, date, mealType);
        }
    }
}
