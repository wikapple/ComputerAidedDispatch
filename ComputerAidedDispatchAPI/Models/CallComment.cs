using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models;

public class CallComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Comment { get; set; }

    [Required]
    public DateTime TimeCreated { get; set; }

    [Required]
    [ForeignKey("CallForService")]
    public int CallId { get; set; }
    public CallForService CallForService { get; set; }

    [ForeignKey("ApplicationUser")]
    public string userId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
