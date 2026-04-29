using HostelManagementSystem.Models;

namespace HostelManagementSystem.Services
{
    public interface IFeeService
    {
        void AddFee(Fee fee);
        List<Fee> GetAllFees();
        Fee? GetById(int id);
        void PayFee(int feeId);
        void EditFee(Fee fee);
        void DeleteFee(int feeId);
    }
}
