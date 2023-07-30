using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Models
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }
    }
}
