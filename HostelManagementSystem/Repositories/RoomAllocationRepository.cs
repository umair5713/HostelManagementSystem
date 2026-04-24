using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories

{
    public class RoomAllocationRepository : IRoomAllocationRepository
    {
        private readonly AppDbContext _context;

        public RoomAllocationRepository(AppDbContext context)
        {
            _context = context;
        }

        // ALLOCATE ROOM — update student's RoomNo in tbl_students
        public void AllocateRoom(int studentId, string roomNo)
        {
            _context.Database.ExecuteSqlRaw(
                "UPDATE tbl_students SET RoomNo = NULL WHERE StudentID = {0}", 
                studentId
            //roomNo,

            );
        }

        // DEALLOCATE ROOM — clear student's RoomNo
        public void DeallocateRoom(int studentId)
        {
            _context.Database.ExecuteSqlRaw(
                "UPDATE tbl_students SET RoomNo = '' WHERE StudentID = {0}",
                studentId
            );
        }

        // GET ALL STUDENTS WITH ROOMS
        public List<Student> GetAllRooms()
        {
            return _context.Students
                      .FromSqlRaw("SELECT StudentID, StudentName, RoomNo, FeeStatus FROM tbl_students WHERE RoomNo IS NOT NULL AND RoomNo != '' ORDER BY RoomNo ASC")
                      .ToList();
        }

        // GET STUDENT BY ROOM
        public Student? GetByRoom(string roomNo)
        {
            return _context.Students
                      .FromSqlRaw("SELECT StudentID, StudentName, RoomNo, FeeStatus FROM tbl_students WHERE RoomNo = {0}", roomNo)
                      .FirstOrDefault();
        }

        // CHECK IF ROOM IS ALREADY TAKEN
        public bool IsRoomTaken(string roomNo)
        {
            var result = _context.Students
                            .FromSqlRaw("SELECT StudentID, StudentName, RoomNo, FeeStatus FROM tbl_students WHERE RoomNo = {0}", roomNo)
                            .FirstOrDefault();
            return result != null;
        }       
    }
}
