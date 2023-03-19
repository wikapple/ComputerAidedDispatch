using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models;

public class Unit
{

    public Unit()
    {
     
    }

    [Key]
    [MaxLength(20)]
    public string UnitNumber { get; set; }

    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser UserInfo { get; set; }

    [ForeignKey("CallForService")]
    public int? CallNumber { get; set; }
    public CallForService? CallForService { get; set; }

    
    private string status;
    [Required]
    public string Status { get; set; }

    public DateTime TimeStatusAssigned { get; set; }
    public TimeSpan StatusDuration
    {
        get { return DateTime.Now.Subtract(TimeStatusAssigned); }

    }

}
