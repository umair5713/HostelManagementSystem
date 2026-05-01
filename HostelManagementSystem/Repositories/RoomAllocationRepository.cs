using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

public class RoomAllocationRepository : IRoomAllocationRepository
{
    private readonly AppDbContext _context;

    public RoomAllocationRepository(AppDbContext context)
    {
        _context = context;
    }

    // ALLOCATE ROOM
    public void AllocateRoom(int studentId, string roomNo)
    {
        _context.Database.ExecuteSqlRaw(
            "UPDATE tbl_students SET RoomNo = {0} WHERE StudentID = {1}",
            roomNo,   // ✅ fixed — was setting NULL before
            studentId
        );
    }

    // DEALLOCATE ROOM
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
                  .FromSqlRaw(@"SELECT StudentID, StudentName, Email, PhoneNumber, CNIC, Semester, RoomNo 
                                FROM tbl_students 
                                WHERE RoomNo IS NOT NULL AND RoomNo != '' 
                                ORDER BY RoomNo ASC")
                  .ToList();
    }

    // GET STUDENT BY ROOM
    public Student? GetByRoom(string roomNo)
    {
        return _context.Students
                  .FromSqlRaw(@"SELECT StudentID, StudentName, Email, PhoneNumber, CNIC, Semester, RoomNo 
                                FROM tbl_students 
                                WHERE RoomNo = {0}", roomNo)
                  .FirstOrDefault();
    }

    // CHECK IF ROOM IS ALREADY TAKEN
    public bool IsRoomTaken(string roomNo)
    {
        var result = _context.Students
                        .FromSqlRaw(@"SELECT StudentID, StudentName, Email, PhoneNumber, CNIC, Semester, RoomNo 
                                      FROM tbl_students 
                                      WHERE RoomNo = {0} AND RoomNo != ''", roomNo)
                        .FirstOrDefault();
        return result != null;
    }
}