using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;

public class DispatcherReadDTO{
    public string DispatchNumber { get; set; }
    public string Name {get; set;}
}