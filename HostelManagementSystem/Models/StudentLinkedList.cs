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
    }
}
