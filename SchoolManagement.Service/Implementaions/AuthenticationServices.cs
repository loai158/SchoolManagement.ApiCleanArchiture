using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Data.Entities;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Data.Helper;
using SchoolManagement.Infrastructure.UnitOfWorks;
using SchoolManagement.Service.Abstacts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Service.Implementaions
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationServices(JwtSettings jwtSettings, IUnitOfWork unitOfWork, ConcurrentDictionary<string, RefreshToken> userRefreshToken, UserManager<ApplicationUser> userManager)
        {
            this._jwtSettings = jwtSettings;
            this._unitOfWork = unitOfWork;
            this._userRefreshToken = userRefreshToken;
            this._userManager = userManager;
        }


        public async Task<JwtAuthResult> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.durationInMinutes),
                signingCredentials: signingCredentials
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GetRefreshToken(user);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiredOn = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtSecurityToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                ApplicationUserId = user.Id
            };

            await _unitOfWork.Repositry<UserRefreshToken>().Create(userRefreshToken);
            await _unitOfWork.CompleteAsync();

            var response = new JwtAuthResult();
            response.AccessToken = accessToken;
            response.refreshToken = GetRefreshToken(user);
            return response;
        }



        private RefreshToken GetRefreshToken(ApplicationUser user)
        {
            var refreshToken = new RefreshToken
            {
                ExpiredAt = DateTime.Now.AddMonths(_jwtSettings.RefreshTokenExpireDate),
                UserName = user.UserName,
                TokenString = GenrateRefreshToken()
            };
            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenrateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
