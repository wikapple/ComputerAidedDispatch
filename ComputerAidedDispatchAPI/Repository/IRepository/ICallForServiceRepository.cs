using ComputerAidedDispatchAPI.Models;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface ICallForServiceRepository
    {
        Task<CallForService> UpdateAsync(CallForService callForService);
    }
}
