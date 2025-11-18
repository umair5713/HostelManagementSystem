using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class StudentMealRepository:IStudentMealRepository
    {
        private List<StudentMeal> meals = new List<StudentMeal>();
        public void AcceptMeal(StudentMeal meal)
        {
            if (!HasAccepted(meal.StudentId, meal.Date, meal.MealType))
            {
                meals.Add(meal);
            }
        }

        public List<StudentMeal> GetMealsByStudent(int studentId)
        {
            return meals.Where(m => m.StudentId == studentId).ToList();
        }

        public bool HasAccepted(int studentId, string date, string mealType)
        {
            return meals.Any(m => m.StudentId == studentId && m.Date == date && m.MealType == mealType);
        }
    }
}
