using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models;

public class CallForService
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public DateTime DateTimeCreated { get; set; }
    public TimeSpan DurationSinceCreated
    {
        get
        {
            return DateTime.Now.Subtract(DateTimeCreated);
        }
    }


    [Required]
    [MaxLength(100)]
    public string CallType { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }
    public string Status 
    {
        get
        {
            if(Units == null || !Units.Any())
            {
                return "Created";
            }
            else if(Units.Any(unit => unit.Status == "On Scene"))
            {
                return "On Scene";
            }
            else if(Units.Any(unit => unit.Status == "En Route"))
            {
                return "En Route";
            }
            else
            {
                return "Assigned";
            }
        }
    }
    public string? Caller_info { get; set; }
    public string Description { get; set; }
    public List<CallComment>? CallComments { get; set; }
    public List<Unit>? Units { get; set; }

}
