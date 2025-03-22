using SchoolManagement.Data.Entities;
using SchoolManagement.Data.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
namespace SchoolManagement.Service.Abstacts
{
    public interface IAuthenticationServices
    {
        //  public Task<string> RegisterAsync(ApplicationUser applicationUser);
        public Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
        public RefreshToken GenerateRefreshToken();
    }
}
