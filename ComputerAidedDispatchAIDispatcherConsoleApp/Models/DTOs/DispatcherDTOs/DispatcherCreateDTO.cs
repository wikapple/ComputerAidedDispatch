using System.ComponentModel.DataAnnotations;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.DispatcherDTOs;

public class DispatcherCreateDTO
{
    [MaxLength(10)]
    public string DispatcherNumber { get; set; }
    public string UserId { get; set; }
}