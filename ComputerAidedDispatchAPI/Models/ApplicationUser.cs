using Microsoft.AspNetCore.Identity;

namespace ComputerAidedDispatchAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public List<CallComment>? CallComments { get; set; }

        public Unit? ThisUnit { get; set; }
        public Dispatcher? ThisDispatcher { get; set; }
    }
}
