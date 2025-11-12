namespace HostelManagementSystem.Models
{
    public class RoomQueue
    {
        private RoomQueueNode front;
        private RoomQueueNode back;

        public void enqueue(Student student)
        {
            RoomQueueNode newnode = new RoomQueueNode { Data = student, next = null };

            if (front == null)
            {
                front = back = newnode;
            }
            else
            {
                back.next = newnode;
                back = newnode;
            }
        }

        public Student dequeue()
        {
            if (front == null)
            {
                return null;
            }

            Student removedStudent = front.Data;
            front = front.next;

            if (front == null)
                back = null;   // queue is now empty

            return removedStudent;
        }

        public bool empty()
        {
            return front == null;
        }

        public List<Student> get_queue()
        {
            List<Student> list = new List<Student>();
            RoomQueueNode temp = front;

            while (temp != null)
            {
                list.Add(temp.Data);
                temp = temp.next;
            }
            return list;
        }
    }
}

