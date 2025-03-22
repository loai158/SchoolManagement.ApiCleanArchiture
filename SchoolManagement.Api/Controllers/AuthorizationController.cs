using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.Authorization.Commands.Models;
using SchoolManagement.Data.AppMetaData;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : ApiControllerBase
    {



        [HttpPost(Router.Authorization.AddRole)]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
    }
}
