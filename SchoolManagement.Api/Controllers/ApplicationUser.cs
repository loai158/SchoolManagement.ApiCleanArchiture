using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.User.Commands.Models;
using SchoolManagement.Core.Features.User.Queries.Models;
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
        [HttpGet(Router.ApplicationUserRouting.Paginate)]
        public async Task<IActionResult> GetAllUsersPaginated([FromQuery] GetUsersPaginatedQuery quary)
        {
            var response = await Mediator.Send(quary);
            return Ok(response);
        }
        [HttpGet(Router.ApplicationUserRouting.GetById)]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPost(Router.ApplicationUserRouting.Edit)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        [HttpPost(Router.ApplicationUserRouting.ChangePassword)]
        public async Task<IActionResult> CahngeUserPassword([FromBody] ChangeUserPAsswordCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        [HttpDelete(Router.ApplicationUserRouting.Delete)]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var response = await Mediator.Send(new DeleteUserCommand(id));
            return NewResult(response);
        }
    }
}
