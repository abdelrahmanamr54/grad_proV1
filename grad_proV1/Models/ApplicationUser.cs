using Microsoft.AspNetCore.Identity;

namespace grad_proV1.Models
{
    public class ApplicationUser : IdentityUser
    {

       // public int Id { get; set; }
        public string Address { get; set; }
        public int? Provider { get; set; }

    }
}
