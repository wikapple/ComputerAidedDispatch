using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;

namespace ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs
{
    public class CallForServiceReadDTO
    {
        public int Id { get; set; }
        public string CallType { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int NumberOfUnitsAssigned { get; set; }
    }
}
