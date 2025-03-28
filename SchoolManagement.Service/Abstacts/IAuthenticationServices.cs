using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Data.Helper;
using System.IdentityModel.Tokens.Jwt;
namespace SchoolManagement.Service.Abstacts
{
    public interface IAuthenticationServices
    {
        //  public Task<string> RegisterAsync(ApplicationUser applicationUser);
        public Task<JwtAuthResult> GetJwtToken(ApplicationUser user);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        public Task<JwtAuthResult> GetRefreshToken(ApplicationUser user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);

        public RefreshToken GetRefreshToken(string refreshToken);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<string> ValidateToken(string AccessToken);
        public Task<string> ConfirmEmail(int? userId, string? code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ConfirmResetPassword(string Code, string Email);
        public Task<string> ResetPassword(string Email, string Password);
    }
}
