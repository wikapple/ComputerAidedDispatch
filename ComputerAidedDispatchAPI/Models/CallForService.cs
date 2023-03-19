using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models;

public class CallForService
{

    public CallForService()
    {
        DateTimeCreated= DateTime.Now;
        TimeStatusAssigned= DateTime.Now;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public DateTime DateTimeCreated { get; set; }

    public DateTime TimeStatusAssigned { get; set; }

    public TimeSpan DurationSinceTimeAssigned 
    {
        get
        {
            return DateTime.Now.Subtract(TimeStatusAssigned);
        }
    }

    [Required]
    [MaxLength(100)]
    public string CallType { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }
    [Required]
    [MaxLength(50)]
    public String Status { get; set; }
    public string? Caller_info { get; set; }
    public string Description { get; set; }
    public List<CallComment>? CallComments { get; set; }
    public List<Unit>? Units { get; set; }

}
