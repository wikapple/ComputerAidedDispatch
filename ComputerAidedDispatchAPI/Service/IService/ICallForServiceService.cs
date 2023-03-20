using ComputerAidedDispatchAPI.Models.DTOs.CallCommentDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;

namespace ComputerAidedDispatchAPI.Service.IService
{
    public interface ICallForServiceService
    {
        public Task<CallForServiceReadDTO?> GetAsync(int callId);
        public Task<CallForServiceDetailsReadDTO?> GetDetailsAsync(int callId);
        public Task<List<CallForServiceReadDTO>> GetAllAsync(string? status = null);
        public Task<List<CallForServiceDetailsReadDTO>> GetAllDetailsAsync(string? status = null);
        public Task<CallForServiceReadDTO?> CreateAsync(CallForServiceCreateDTO createDTO);
        public Task<CallForServiceReadDTO?> UpdateAsync(CallForServiceUpdateDTO updateDTO);
        public Task DeleteAsync(int callId);
        public Task<CallForServiceReadDTO?> UpdateStatus(int callId, string status);
        public void AssignUnits(int callId, List<string> unitIds);
        public void RemoveUnits(int callId, List<string> unitIds);
        public Task PostComment(int callId, CreateCallCommentDTO createCommentDTO);

    }
}
