using ComputerAidedDispatchAPI.Models.DTOs.CallCommentDTOs;

namespace ComputerAidedDispatchAPI.Service.IService
{
    public interface ICallCommentService
    {
        public Task<CallCommentReadDTO?> GetAsync(int callId);
        public Task<List<CallCommentReadDTO>> GetAllAsync(int? callId = null);
        public Task<CallCommentReadDTO> CreateAsync(CreateCallCommentDTO createCallCommentDTO, string userName);
        
    }
}
