using System.ComponentModel.DataAnnotations;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;

public class UnitCreateDTO
{
    [Required]
    [MaxLength(20)]
    public string UnitNumber { get; set; }
    [Required]
    public string UserId { get; set; }

    public string Status { get; set; }

    public UnitCreateDTO()
    {
        Status = "Unavailable";
    }

}
