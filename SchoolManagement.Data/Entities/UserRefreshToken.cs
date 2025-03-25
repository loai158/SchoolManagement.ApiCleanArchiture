using SchoolManagement.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Data.Entities
{

    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? JwtId { get; set; }
        public DateTime AddedTime { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredOn { get; set; }
        public bool IsRevoked { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [InverseProperty(nameof(ApplicationUser.UserRefreshTokens))]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
