namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs
{
    public class CallForServiceUpdateDTO
    {
        public int Id { get; set; }
        public string CallType { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string? Caller_info { get; set; }
    }
}
