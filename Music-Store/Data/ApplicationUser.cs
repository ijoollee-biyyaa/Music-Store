using Microsoft.AspNetCore.Identity;

namespace Music_Store.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
