using ComputerAidedDispatchAPI.Models;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface IDispatcherRepository
    {
        Task<Dispatcher> UpdateAsync(Dispatcher dispatcher);
    }
}
