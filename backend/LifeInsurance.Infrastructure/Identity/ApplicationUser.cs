using Microsoft.AspNetCore.Identity;

namespace LifeInsurance.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
