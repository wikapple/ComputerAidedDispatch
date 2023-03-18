using ComputerAidedDispatchAPI.Models;
using System.Linq.Expressions;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface IUnitRepository
    {
        public Task CreateAsync(Unit unit);
        public Task<Unit> GetAsync(Expression<Func<Unit, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        public Task<List<Unit>> GetAllAsync(Expression<Func<Unit, bool>>? filter = null, string? includeProperties = null);

        public Task RemoveAsync(Unit unit);
        public Unit? GetDetails(string unitNumber);
        public Task<Unit> UpdateAsync(Unit unit);
    }
}
