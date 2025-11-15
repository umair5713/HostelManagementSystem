using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private StudentNode head;

        public void AddStudent(Student student)
        {
            StudentNode newnode = new StudentNode
            {
                Data = student,
                Next = null
            };

            if (head == null)
            {
                head = newnode;
                return;
            }

            StudentNode current = head;
            while (current.Next != null)
                current = current.Next;

            current.Next = newnode;
        }

        public List<Student> GetStudents()
        {
            List<Student> list = new List<Student>();
            StudentNode temp = head;

            while (temp != null)
            {
                list.Add(temp.Data);
                temp = temp.Next;
            }

            return list;
        }

        public void SortByID()
        {
            head = MergeSort(head);
        }

        public StudentNode MergeSort(StudentNode h)
        {
            if (h == null || h.Next == null)
                return h;

            StudentNode mid = GetMiddle(h);

            StudentNode left = h;
            StudentNode right = mid.Next;
            mid.Next = null;

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        public StudentNode Merge(StudentNode left, StudentNode right)
        {
            if (left == null) return right;
            if (right == null) return left;

            StudentNode dummy = new StudentNode();
            StudentNode temp = dummy;

            while (left != null && right != null)
            {
                if (left.Data.StudentID <= right.Data.StudentID)
                {
                    temp.Next = left;
                    temp = left;
                    left = left.Next;
                }
                else
                {
                    temp.Next = right;
                    temp = right;
                    right = right.Next;
                }
            }

            while (left != null)
            {
                temp.Next = left;
                temp = left;
                left = left.Next;
            }

            while (right != null)
            {
                temp.Next = right;
                temp = right;
                right = right.Next;
            }

            return dummy.Next;
        }

        public StudentNode GetMiddle(StudentNode h)
        {
            StudentNode slow = h;
            StudentNode fast = h.Next;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return slow;
        }

    }
}
