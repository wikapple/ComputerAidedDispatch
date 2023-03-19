using System.ComponentModel.DataAnnotations;

namespace ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;

public class DispatcherCreateDTO{
    [MaxLength(10)]
    public string DispatcherNumber { get; set;}
}