using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.Authentication.Commands.Models;
using SchoolManagement.Data.AppMetaData;
namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        [HttpPost(Router.Authentication.SignIn)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand Command)
        {
            var response = await Mediator.Send(Command);


            return NewResult(response);
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
