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
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>
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
                if (!signInResult)
                {
                    return BadRequest<JwtAuthResult>(" User Name Or PassWord Is Wrong");
                }
                else
                {
                    var accessToken = await _authenticationServices.CreateJwtToken(user);

                    return Success(accessToken);
                }

            }
        }
    }
}
