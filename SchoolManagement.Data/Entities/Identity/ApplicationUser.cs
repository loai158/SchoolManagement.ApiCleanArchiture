using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string FullName { get; set; }
        [InverseProperty(nameof(UserRefreshToken.ApplicationUser))]
        public List<UserRefreshToken>? UserRefreshTokens { get; set; }
    }
}
