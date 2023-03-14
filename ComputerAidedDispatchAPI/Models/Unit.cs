using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models;

public class Unit
{

    public Unit()
    {
        timeStatusAssigned = DateTime.Now;
     
    }

    [Key]
    [MaxLength(20)]
    public string UnitNumber { get; set; }

    [ForeignKey("ApplicationUser")]
    public int UserId { get; set; }
    public ApplicationUser UserInfo { get; set; }

    [ForeignKey("CallForService")]
    public int? CallNumber { get; set; }
    public CallForService? CallForService { get; set; }

    
    private string status;
    [Required]
    public string Status
    {
        get { return status; }
        set {
            timeStatusAssigned = DateTime.Now;
                status = value;
            }
    }

    private DateTime timeStatusAssigned;

    public DateTime TimeStatusAssigned
    {
        get { return timeStatusAssigned; }

    }

    public TimeSpan StatusDuration
    {
        get { return DateTime.Now.Subtract(timeStatusAssigned); }

    }

}
