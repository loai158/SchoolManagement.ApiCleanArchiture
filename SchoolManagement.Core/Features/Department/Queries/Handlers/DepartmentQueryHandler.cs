using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Department.Queries.Models;
using SchoolManagement.Core.Features.Department.Queries.Responses;
using SchoolManagement.Core.Resources;
using SchoolManagement.Core.Wrapper;
using SchoolManagement.Data.Entities;
using SchoolManagement.Service.Abstacts;
using System.Linq.Expressions;

namespace SchoolManagement.Core.Features.Department.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
           IRequestHandler<GetDeparmentByIdQuery, Response<GetAllDepartmentResponse>>
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly IStudentServices _studentServices;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _istringLocalizer;

        public DepartmentQueryHandler(IDepartmentServices departmentServices, IStudentServices studentServices, IStringLocalizer<SharedResources> istringLocalizer, IMapper mapper)
        {
            this._departmentServices = departmentServices;
            this._studentServices = studentServices;
            this._mapper = mapper;
            this._istringLocalizer = istringLocalizer;
        }


        public async Task<Response<GetAllDepartmentResponse>> Handle(GetDeparmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentServices.GetDepartmentById(request.Id);
            if (department == null)
            {
                return NotFound<GetAllDepartmentResponse>(_istringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var result = _mapper.Map<GetAllDepartmentResponse>(department);
            Expression<Func<Student, StudentList>> expression = e => new StudentList(e.StudID, e.Name);
            var studentQuarable = _studentServices.GetStudentsByDeptQuarable(request.Id);
            var PaginatedList = await studentQuarable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            result.Students = PaginatedList;
            return Success(result);
        }


    }
}
