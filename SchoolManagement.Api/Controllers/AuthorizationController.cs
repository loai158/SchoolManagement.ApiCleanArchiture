using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.Authorization.Commands.Models;
using SchoolManagement.Core.Features.Authorization.Queries.Models;
using SchoolManagement.Data.AppMetaData;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    // [Authorize(Roles = "Admin,Super Admin")]
    public class AuthorizationController : ApiControllerBase
    {
        [HttpPost(Router.Authorization.AddRole)]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }

        [HttpDelete(Router.Authorization.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet(Router.Authorization.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRolesQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        [HttpGet(Router.Authorization.GetById)]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.Authorization.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] string userId)
        {
            var response = await Mediator.Send(new MangeUserRolesQuery() { UserId = userId });
            return NewResult(response);
        }
        [HttpPut(Router.Authorization.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.Authorization.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpadteUserClaimCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.Authorization.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] string userId)
        {
            var response = await Mediator.Send(new MangeUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }
    }
}
