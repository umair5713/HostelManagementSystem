using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class StudentMealService:IStudentMealService
    {
        private readonly IStudentMealRepository _repo;

        public StudentMealService(IStudentMealRepository repo)
        {
            _repo = repo;
        }

        public void AcceptMeal(int studentId, string date, string mealType)
        {
            var meal = new StudentMeal
            {
                StudentId = studentId,
                Date = date,
                MealType = mealType,
                Time = DateTime.Now
            };
            _repo.AcceptMeal(meal);
        }

        public List<StudentMeal> GetMealsByStudent(int studentId)
        {
            return _repo.GetMealsByStudent(studentId);
        }

        public bool HasAccepted(int studentId, string date, string mealType)
        {
            return _repo.HasAccepted(studentId, date, mealType);
        }
    }
}
