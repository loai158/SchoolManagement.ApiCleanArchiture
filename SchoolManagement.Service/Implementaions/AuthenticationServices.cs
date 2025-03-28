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
        public async Task<JwtAuthResult> GetJwtToken(ApplicationUser user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiredOn = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.ToString(),
                Token = accessToken,
                ApplicationUserId = user.Id
            };
            await _unitOfWork.Repositry<UserRefreshToken>().Create(userRefreshToken);

            var response = new JwtAuthResult();
            response.refreshToken = refreshToken;
            response.AccessToken = accessToken;
            return response;
        }
        public async Task<JwtAuthResult> GetRefreshToken(ApplicationUser user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpiredAt = (DateTime)expiryDate;
            response.refreshToken = refreshTokenResult;
            return response;

        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(ApplicationUser applicationUser)
        {
            var claims = await GetClaims(applicationUser);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.secretKey)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

            var userRefreshToken = await _unitOfWork.Repositry<UserRefreshToken>().GetOne(filter: x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.ApplicationUserId == userId);
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiredOn < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                _unitOfWork.Repositry<UserRefreshToken>().Edit(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiredOn;
            return (userId, expirydate);
        }
        public RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                ExpiredAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = userName,
                TokenString = GenrateRefreshToken()
            };
            return refreshToken;

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
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }
        private string GenrateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),

            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }

        public async Task<string> ValidateToken(string AccessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.secretKey)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(AccessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Task<string> ConfirmEmail(int? userId, string? code)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SendResetPasswordCode(string Email)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                //user
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                //Generate Random Number

                //Random generator = new Random();
                //string randomNumber = generator.Next(0, 1000000).ToString("D6");
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                //update User In Database Code
                //user.Code = randomNumber;
                //var updateResult = await _userManager.UpdateAsync(user);
                //if (!updateResult.Succeeded)
                //    return "ErrorInUpdateUser";
                //var message = "Code To Reset Passsword : " + user.Code;
                ////Send Code To  Email 
                //await _emailsService.SendEmail(user.Email, message, "Reset Password");
                //await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return "Failed";
            }
        }

        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            //Get User
            //user
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            //Decrept Code From Database User Code
            //var userCode = user.Code;
            ////Equal With Code
            //if (userCode == Code) return "Success";
            return "Failed";
        }

        public async Task<string> ResetPassword(string Email, string Password)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                await _userManager.RemovePasswordAsync(user);
                if (!await _userManager.HasPasswordAsync(user))
                {
                    await _userManager.AddPasswordAsync(user, Password);
                }
                _unitOfWork.CommitTransactionAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return "Failed";
            }
        }
    }
}
