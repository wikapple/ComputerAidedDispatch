namespace ComputerAidedDispatchAPI.Models.DTOs
{
    public class RegistrationRequestDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
