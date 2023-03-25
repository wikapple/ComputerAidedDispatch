using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UserDTOs;

namespace ComputerAidedDispatchAPI.Service.IService
{
    public interface ICadSharedService
    {
        Task<UnitReadDTO> AssignCallAsync(string unitId, int? callId);
        Task<bool> DoesCallForServiceExist(int callNumber);
        bool DoesUserIdExist(string userId);
        Task<UnitReadDTO> GetByUnitNumberAsync(string unitId);
        string? GetUserIdByUserName(string userName);
        Task<UserDTO?> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<UnitReadDTO> UpdateStatusAsync(string unitId, string v);
    }
}
