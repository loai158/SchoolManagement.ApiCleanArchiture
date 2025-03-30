using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authentication.Commands.Models;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Data.Helper;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
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
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return BadRequest<JwtAuthResult>(" User Name Not Exist");
            }
            else
            {
                var signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!user.EmailConfirmed)
                    BadRequest<JwtAuthResult>("Confirm Email is not");
                if (!signInResult)
                {
                    return BadRequest<JwtAuthResult>(" User Name Or PassWord Is Wrong");
                }
                else
                {
                    var accessToken = await _authenticationServices.GetJwtToken(user);

                    return Success(accessToken);
                }

            }
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationServices.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationServices.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>("wrong algorithm");
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(" TokenIsNotExpired");
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>("RefreshTokenIsNotFound");
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>("RefreshTokenIsExpired");
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationServices.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }
    }
}
