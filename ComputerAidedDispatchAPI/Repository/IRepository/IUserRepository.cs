using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UserDTOs;
using System.Linq.Expressions;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO?> Register(RegistrationRequestDTO registrationRequestDTO);
        public bool DoesDefaultSystemUserExist();
        public bool DoesDefaultTestUserExist();
        ApplicationUser? GetUser(string userId);
        ApplicationUser? GetUser(Expression<Func<ApplicationUser, bool>> filter);
    }
}
