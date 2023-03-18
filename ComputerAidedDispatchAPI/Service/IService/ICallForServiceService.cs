using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;

namespace ComputerAidedDispatchAPI.Service.IService
{
    public interface ICallForServiceService
    {
        public Task<CallForServiceReadDTO?> GetAsync(int callId);
    }
}
