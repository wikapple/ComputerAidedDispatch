using System.Runtime.CompilerServices;

namespace ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs
{
    public class UnitUpdateDTO
    {
        public string UnitNumber { get; set; }
        public int? CallNumber { get; set; }
        public string Status { get; set; }

    }
}
