using HostelManagementSystem.Models;
using HostelManagementSystem.Repositories;

namespace HostelManagementSystem.Services
{
    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _repo;

        public FeeService(IFeeRepository repo)
        {
            _repo = repo;
        }

        public void AddFee(Fee fee)
        {
            _repo.AddFee(fee);
        }

        public List<Fee> GetAllFees()
        {
            return _repo.GetFees();
        }
        public Fee? GetById(int id)
        {
            return _repo.GetById(id);
        }
        public void EditFee(Fee fee)
        {
            _repo.EditFee(fee);
        }
        public void PayFee(int feeId)
        {
            _repo.MarkAsPaid(feeId);
        }

        public void DeleteFee(int feeId)
        {
            _repo.DeleteFee(feeId);
        }
    }
}
