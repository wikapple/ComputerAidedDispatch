using System.Linq.Expressions;
using ComputerAidedDispatchAPI.Models;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface IDispatcherRepository
    {

        public Task<Dispatcher?> CreateAsync(Dispatcher dispatcher);
        public Task<Dispatcher> GetAsync(Expression<Func<Dispatcher, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        public Task<List<Dispatcher>> GetAllAsync(Expression<Func<Dispatcher, bool>>? filter = null, string? includeProperties = null);

        public Task<List<Dispatcher>> GetAllDetailsAsync(Expression<Func<Dispatcher, bool>>? filter = null);
        public Task RemoveAsync(Dispatcher dispatcher);
        public Task<Dispatcher?> GetDetails(string dispatcherNumber);
        public Task<Dispatcher> UpdateAsync(Dispatcher dispatcher);
    }
}
