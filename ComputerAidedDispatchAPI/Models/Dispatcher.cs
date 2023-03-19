using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerAidedDispatchAPI.Models
{
    public class Dispatcher
    {
        [Key]
        [MaxLength(10)]
        public string DispatcherNumber { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser UserInfo { get; set; }
        
    }
}
