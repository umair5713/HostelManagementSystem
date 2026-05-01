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
                "EXEC sp_AddFee @StudentID, @Amount, @DueDate,@Month",
                new SqlParameter("@StudentID", fee.StudentID),
                new SqlParameter("@Amount", fee.Amount),
                new SqlParameter("@DueDate", fee.DueDate),
                new SqlParameter("@Month", fee.Month)
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
                "UPDATE tbl_fees SET IsPaid = 1, PaidDate = @paidDate WHERE FeeID = @id",
                new SqlParameter("@paidDate", DateTime.Now),
                new SqlParameter("@id", feeId)
            );
        }
        public void EditFee(Fee fee)
        {
            _context.Database.ExecuteSqlRaw(
                @"UPDATE tbl_fees 
          SET Amount   = @amount,
              Month    = @month,
              DueDate  = @dueDate,
              IsPaid   = @isPaid,
              PaidDate = @paidDate
          WHERE FeeID  = @id",
                new SqlParameter("@amount", fee.Amount),
                new SqlParameter("@month", fee.Month),
                new SqlParameter("@dueDate", fee.DueDate),
                new SqlParameter("@isPaid", fee.IsPaid),
                new SqlParameter("@paidDate", fee.IsPaid ? (object)DateTime.Now : DBNull.Value),
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
