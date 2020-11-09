using Microsoft.AspNetCore.Identity;

namespace MyPortfolio.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}