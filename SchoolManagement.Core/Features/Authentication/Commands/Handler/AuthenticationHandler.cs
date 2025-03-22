using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authentication.Commands.Models;
using SchoolManagement.Core.Features.Authentication.Commands.Responses;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Service.Abstacts;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolManagement.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<SignInResponse>>
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationHandler(IAuthenticationServices authenticationServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._authenticationServices = authenticationServices;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public async Task<Response<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return BadRequest<SignInResponse>(" User Name Not Exist");
            }
            else
            {
                var signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!signInResult)
                {
                    return BadRequest<SignInResponse>(" User Name Or PassWord Is Wrong");
                }
                else
                {
                    var accessToken = await _authenticationServices.CreateJwtToken(user);
                    var rolesList = await _userManager.GetRolesAsync(user);

                    var signInResponse = new SignInResponse();
                    signInResponse.Token = new JwtSecurityTokenHandler().WriteToken(accessToken);
                    signInResponse.IsAuthenticated = true;
                    signInResponse.Email = user.Email;
                    signInResponse.Username = user.UserName;
                    //   signInResponse.ExpiresOn = accessToken.ValidTo;
                    signInResponse.Roles = rolesList.ToList();
                    if (user.RefreshTokens.Any(t => t.IsActive))
                    {
                        var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                        signInResponse.RefreshToken = activeRefreshToken.Token;
                        signInResponse.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
                    }
                    else
                    {
                        var refreshToken = _authenticationServices.GenerateRefreshToken();
                        signInResponse.RefreshToken = refreshToken.Token;
                        signInResponse.RefreshTokenExpiration = refreshToken.ExpiresOn;
                        user.RefreshTokens.Add(refreshToken);
                        await _userManager.UpdateAsync(user);
                    }

                    return Success(signInResponse);
                }

            }
        }
    }
}
