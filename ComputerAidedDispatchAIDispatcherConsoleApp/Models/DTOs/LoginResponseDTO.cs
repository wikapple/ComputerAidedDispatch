using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UserDTOs;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
