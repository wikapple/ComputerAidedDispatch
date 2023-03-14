using ComputerAidedDispatchAPI.Models.DTOs;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO?> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
