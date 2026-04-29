using HostelManagementSystem.Models;

namespace HostelManagementSystem.Repositories
{
    public interface IFeeRepository
    {
        void AddFee(Fee fee);
        List<Fee> GetFees();
        Fee? GetById(int id);
        void MarkAsPaid(int feeId);
        void DeleteFee(int feeId);
        void EditFee(Fee fee);
    }
}
