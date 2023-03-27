using System.ComponentModel.DataAnnotations;

namespace ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs
{
    public class CallForServiceCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string CallType { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        public string? Caller_info { get; set; }
        public string Description { get; set; }
        public List<string>? AssignedUnitIds { get; set; }
    }
}
