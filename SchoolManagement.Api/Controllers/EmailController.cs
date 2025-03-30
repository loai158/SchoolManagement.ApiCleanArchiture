using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.Email.Commands.Models;
using SchoolManagement.Data.AppMetaData;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    public class EmailController : ApiControllerBase
    {
        [HttpPost(Router.EmailsRoute.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
