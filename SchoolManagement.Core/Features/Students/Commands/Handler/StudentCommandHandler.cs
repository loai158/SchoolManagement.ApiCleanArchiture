using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Students.Commands.Models;
using SchoolManagement.Core.Resources;
using SchoolManagement.Data.Entities;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Students.Commands.Handler
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<String>>,
     IRequestHandler<EditStudentCommand, Response<String>>,
     IRequestHandler<DeleteStudentCommand, Response<String>>
    {
        private readonly IStudentServices studentServices;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> _istringLocalizer;

        public StudentCommandHandler(IStudentServices studentServices, IStringLocalizer<SharedResources> istringLocalizer, IMapper mapper)
        {
            this.studentServices = studentServices;
            _istringLocalizer = istringLocalizer;
            this.mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //map first before adding
            var studentMapper = mapper.Map<Student>(request);
            //add
            var result = await studentServices.AddStudentAsync(studentMapper);

            if (result == "Success")
            {
                return Created("");
            }
            else
            {
                return BadRequest<String>("Name Already Exist");
            }
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            // check if it exist first
            var studnt = await studentServices.GetStudentById(request.Id);
            if (studnt == null)
                return NotFound<String>("student not fouond");

            var studentMapper = mapper.Map(request, studnt);
            var result = studentServices.Edit(studentMapper);
            if (result == "success")
            {
                return Created("Edit Successfully");
            }
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var studnt = await studentServices.GetStudentById(request.Id);
            if (studnt == null)
                return NotFound<String>("student not found");
            else
            {
                var result = studentServices.Delete(request.Id);

                if (await result.ConfigureAwait(false) == "faild")

                    return BadRequest<String>();

                else
                    return Deleted("");
            }



            //}

            //async Task<Response<string>> IRequestHandler<DeleteStudentCommand, Response<string>>.Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            //{
            //    var studnt = await studentServices.GetById(request.id);
            //    if (studnt == null)
            //        return NotFound<String>("student not found");
            //    else
            //    {
            //        var result = await studentServices.DeleteAsync(request.id);
            //        if (result == "faild")
            //            return BadRequest<String>();

            //        else
            //            return Deleted("Deleted Successfully");
            //    }
            //}
        }
    }
}