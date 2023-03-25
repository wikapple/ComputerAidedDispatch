using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.DispatcherDTOs;

public class DispatcherReadDTO
{
    public string DispatcherNumber { get; set; }
    public string Name { get; set; }
}