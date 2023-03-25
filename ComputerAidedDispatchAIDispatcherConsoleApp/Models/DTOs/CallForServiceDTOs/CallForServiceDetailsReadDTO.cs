using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallCommentDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;

public class CallForServiceDetailsReadDTO
{
    public int Id { get; set; }
    public string CallType { get; set; }
    public string Address { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string? Caller_info { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public List<UnitReadDTO>? Units { get; set; }
    public List<CallCommentReadDTO>? CallComments { get; set; }
}
