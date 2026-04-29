using HostelManagementSystem.Data;
using HostelManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HostelManagementSystem.Repositories
{
    public class FeeRepository : IFeeRepository
    {
        private readonly AppDbContext _context;

        public FeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddFee(Fee fee)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_AddFee @StudentID, @Amount, @DueDate",
                new SqlParameter("@StudentID", fee.StudentID),
                new SqlParameter("@Amount", fee.Amount),
                new SqlParameter("@DueDate", fee.DueDate)
            );
        }

        public List<Fee> GetFees()
        {
            return _context.Fees
                           .Include(f => f.Student)  
                           .OrderBy(f => f.FeeID)
                           .ToList();
        }

        public Fee? GetById(int id)
        {
            return _context.Fees
                           .Include(f => f.Student)  
                           .FirstOrDefault(f => f.FeeID == id);
        }

        public void MarkAsPaid(int feeId)
        {
            _context.Database.ExecuteSqlRaw(
                "UPDATE tbl_fees SET IsPaid = 1 WHERE FeeID = {0}", feeId
            );
        }
        public void EditFee(Fee fee)
        {
            _context.Database.ExecuteSqlRaw(
                @"UPDATE tbl_fees 
          SET StudentID = @studentId,
              Amount = @amount,
              DueDate = @dueDate,
              IsPaid = @isPaid
          WHERE FeeID = @id",
                new SqlParameter("@studentId", fee.StudentID),
                new SqlParameter("@amount", fee.Amount),
                new SqlParameter("@dueDate", fee.DueDate),
                new SqlParameter("@isPaid", fee.IsPaid),
                new SqlParameter("@id", fee.FeeID)
            );
        }

        public void DeleteFee(int feeId)
        {
            _context.Database.ExecuteSqlRaw(
                "DELETE FROM tbl_fees WHERE FeeID = {0}", feeId
            );
        }
    }
}
