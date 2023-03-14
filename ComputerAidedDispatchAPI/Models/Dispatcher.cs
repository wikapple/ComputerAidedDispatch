using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models
{
    public class Dispatcher
    {
        [Key]
        public string DispatchNumber { get; set; }
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public ApplicationUser UserInfo { get; set; }
        
    }
}
