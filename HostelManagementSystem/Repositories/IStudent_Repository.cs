using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IStudent_Repository
    {
        void add_student(Student student);
        List<Student> GetStudents();
        void SortbyID();
        StudentNode merge_sort(StudentNode h);
        StudentNode merge(StudentNode left, StudentNode right);
        StudentNode get_middle(StudentNode h);

    }
}
