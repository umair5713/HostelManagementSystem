using System.Collections.Generic;

namespace HostelManagementSystem.Models
{
    public class AttendanceStack
    {
        private AttendanceStackNode top;

        // PUSH
        public void Push(AttendanceRecord record)
        {
            AttendanceStackNode newnode = new AttendanceStackNode();
            newnode.Data = record;
            newnode.next = top;
            top = newnode;
        }

        // POP
        public AttendanceRecord Pop()
        {
            if (top == null)
                return null;

            AttendanceRecord removed = top.Data;
            top = top.next;
            return removed;
        }

        // TRAVERSE : GET LIST
        public List<AttendanceRecord> GetAttendanceList()
        {
            List<AttendanceRecord> list = new List<AttendanceRecord>();
            AttendanceStackNode current = top;

            while (current != null)
            {
                list.Add(current.Data);
                current = current.next;
            }

            return list;
        }

        // PEEK — Show last marked student
        public AttendanceRecord Peek()
        {
            return top != null ? top.Data : null;
        }

        // EMPTY CHECK
        public bool IsEmpty()
        {
            return top == null;
        }

        // COUNT
        public int Count()
        {
            int counter = 0;
            AttendanceStackNode current = top;
            while (current != null)
            {
                counter++;
                current = current.next;
            }
            return counter;
        }

        // SEARCH — Check if student attended (linear search)
        public bool Search(string name)
        {
            AttendanceStackNode temp = top;
            while (temp != null)
            {
                if (temp.Data.StudentName.ToLower() == name.ToLower())
                    return true;
                temp = temp.next;
            }
            return false;
        }
    }
}

