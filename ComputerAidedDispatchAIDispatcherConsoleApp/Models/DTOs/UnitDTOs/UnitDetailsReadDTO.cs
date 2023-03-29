using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs
{
    public class UnitDetailsReadDTO
    {
        public string UnitNumber { get; set; }
        public string Name { get; set; }
        public CallForServiceReadDTO? CallForService { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
