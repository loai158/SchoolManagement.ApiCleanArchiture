using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.Department.Queries.Models;
using SchoolManagement.Data.AppMetaData;
namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    public class DepartmentController : ApiControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDeparmentByIdQuery query)
        {
            return NewResult(await Mediator.Send(query));
        }
    }
}
