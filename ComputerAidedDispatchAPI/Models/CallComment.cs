using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models;

public class CallComment
{

    public CallComment()
    {
        DateTimeEntered= DateTime.Now;
    }
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Comment { get; set; }

    [Required]
    public DateTime DateTimeEntered { get; set; }
    [Required]
    [ForeignKey("CallForService")]
    public int CallId { get; set; }
    public CallForService CallForService { get; set; }

    [ForeignKey("ApplicationUser")]
    public string userId { get; set; }
    [Required]
    public ApplicationUser User { get; set; }
}
