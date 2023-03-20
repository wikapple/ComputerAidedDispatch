using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UserDTOs;

namespace ComputerAidedDispatchAPI.Service.IService;

public interface IUserService
{
    bool DoesUserIdExist(string userId);
    public bool IsUniqueUser(string username);
    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    public Task<UserDTO?> Register(RegistrationRequestDTO registrationRequestDTO);
}
