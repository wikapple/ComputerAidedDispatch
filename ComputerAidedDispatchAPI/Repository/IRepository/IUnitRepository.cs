using ComputerAidedDispatchAPI.Models;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface IUnitRepository
    {
        Task<Unit> UpdateAsync(Unit unit);
    }
}
