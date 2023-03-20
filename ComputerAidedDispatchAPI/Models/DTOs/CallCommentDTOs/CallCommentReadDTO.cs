namespace ComputerAidedDispatchAPI.Models.DTOs.CallCommentDTOs
{
    public class CallCommentReadDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public int CallForServiceNumber { get; set; }
    }
}
