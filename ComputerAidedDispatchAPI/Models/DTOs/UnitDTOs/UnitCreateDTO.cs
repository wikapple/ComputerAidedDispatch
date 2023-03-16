using System.ComponentModel.DataAnnotations;

namespace ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;

public class UnitCreateDTO
{
    [Required]
    [MaxLength(20)]
    public string UnitNumber { get; set; }
    [Required]
    public string UserId { get; set; }

}
