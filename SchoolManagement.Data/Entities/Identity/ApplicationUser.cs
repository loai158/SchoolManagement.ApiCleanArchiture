using Microsoft.AspNetCore.Identity;

namespace SchoolManagement.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string FullName { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
