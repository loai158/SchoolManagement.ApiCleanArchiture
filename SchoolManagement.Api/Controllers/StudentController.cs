using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Base;
using SchoolManagement.Core.Features.Students.Commands.Models;
using SchoolManagement.Core.Features.Students.Queries.Models;
using SchoolManagement.Data.AppMetaData;

namespace SchoolManagement.Api.Controllers
{

    [ApiController]
    [Authorize]
    public class StudentController : ApiControllerBase
    {


        //GetAllStudents

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await Mediator.Send(new GetAllStudentsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.StudentRouting.Paginate)]
        public async Task<IActionResult> GetAllStudentsPaginated([FromQuery] GetStudentPaginatedQuary quary)
        {
            var response = await Mediator.Send(quary);
            return Ok(response);
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new GetStudentByIdQuery(id)));
        }
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        [HttpPost(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}
