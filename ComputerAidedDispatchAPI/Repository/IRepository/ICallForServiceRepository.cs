using ComputerAidedDispatchAPI.Models;
using System.Linq.Expressions;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface ICallForServiceRepository
    {
        Task<CallForService> UpdateAsync(CallForService callForService);

        public Task CreateAsync(CallForService callForService);
        public Task<CallForService> GetAsync(Expression<Func<CallForService, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        public Task<List<CallForService>> GetAllAsync(Expression<Func<CallForService, bool>>? filter = null, string? includeProperties = null);

        public Task RemoveAsync(CallForService callForService);

    }
}
