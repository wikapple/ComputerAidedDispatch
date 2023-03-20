using ComputerAidedDispatchAPI.Models.DTOs.UserDTOs;

namespace ComputerAidedDispatchAPI.Models.DTOs
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
