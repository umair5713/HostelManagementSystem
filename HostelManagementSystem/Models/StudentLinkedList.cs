namespace HostelManagementSystem.Models
{
    public class StudentLinkedList
    {
        private StudentNode head;

        public void add_student(Student student)
        {

            StudentNode newnode = new StudentNode();
            newnode.Data = student;
            newnode.next = null;

            if (head == null)
            {
                head = newnode;
                return;
            }

            StudentNode current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = newnode;
        }

        public List<Student> GetStudents()
        {
            List<Student> hostellers = new List<Student>();

            StudentNode current = head;

            while (current != null)
            {
                hostellers.Add(current.Data);
                current = current.next;
            }
            return hostellers;
        }

        public void SortbyID()
        {
            head = merge_sort(head);
        }

        private StudentNode merge_sort(StudentNode h)
        {
            //base case
            if (h == null || h.next == null)
            {
                return h;
            }

            //divide the linked list int 2 halves
            StudentNode mid = get_middle(h);

            StudentNode left = h;
            StudentNode right = mid.next;
            mid.next = null;

            //recursvie calls to sort both halves
            left = merge_sort(left);
            right = merge_sort(right);

            //merge both left and right halves
            StudentNode result = merge(left, right);

            return result;
        }

        private StudentNode merge(StudentNode left, StudentNode right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            StudentNode dummy = new StudentNode();
            StudentNode temp = dummy;

            //merge 2 sorted linked lists
            while (left != null && right != null)
            {
                if (left.Data.StudentID <= right.Data.StudentID)
                {
                    temp.next = left;
                    temp = left;
                    left = left.next;
                }
                else
                {
                    temp.next = right;
                    temp = right;
                    right = right.next;
                }
            }

            while (left != null)
            {
                temp.next = left;
                temp = left;
                left = left.next;
            }

            while (right != null)
            {
                temp.next = right;
                temp = right;
                right = right.next;
            }
            //answer = answer.next;
            return dummy.next;
        }


        private StudentNode get_middle(StudentNode h)
        {
            StudentNode slow = h;
            StudentNode fast = h.next;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            
            return slow;
        }
        
    }
}
