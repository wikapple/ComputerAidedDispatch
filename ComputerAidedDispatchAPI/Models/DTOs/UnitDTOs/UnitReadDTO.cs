namespace ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs
{
    public class UnitReadDTO
    {
        public string UnitNumber { get; set; }

        public int? CallNumber { get; set; }
        public string Status { get; set; }

        public string Name { get; set; }
        public override string ToString()
        {
            return $"Unit Number: {UnitNumber}, Call Number: {CallNumber}, Status: {Status}";
        }
    }
}
