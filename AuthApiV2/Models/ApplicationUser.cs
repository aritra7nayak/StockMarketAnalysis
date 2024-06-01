using Microsoft.AspNetCore.Identity;

namespace AuthApiV2.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
