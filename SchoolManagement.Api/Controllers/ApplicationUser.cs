using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.User.Commands.Models;
using SchoolManagement.Data.AppMetaData;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    public class ApplicationUser : ApiControllerBase
    {
        [HttpPost(Router.ApplicationUserRouting.Register)]
        public async Task<IActionResult> Register([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
