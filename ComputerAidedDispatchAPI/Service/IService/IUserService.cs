using ComputerAidedDispatchAPI.Models.DTOs;

namespace ComputerAidedDispatchAPI.Service.IService;

public interface IUserService
{
    public bool IsUniqueUser(string username);
    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    public Task<UserDTO?> Register(RegistrationRequestDTO registrationRequestDTO);
}
