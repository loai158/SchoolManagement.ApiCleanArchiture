using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Students.Queries.Models;
using SchoolManagement.Core.Features.Students.Queries.Responses;
using SchoolManagement.Core.Resources;
using SchoolManagement.Core.Wrapper;
using SchoolManagement.Data.Entities;
using SchoolManagement.Service.Abstacts;
using System.Linq.Expressions;

namespace SchoolManagement.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
        IRequestHandler<GetAllStudentsQuery, Response<IEnumerable<GetAllStudentsResponse>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetAllStudentsResponse>>,
        IRequestHandler<GetStudentPaginatedQuary, PaginatedResult<GetAllStudentsPaginamtedResponse>>


    {
        private readonly IStudentServices studentServices;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> istringLocalizer;

        public StudentQueryHandler(IStudentServices studentServices, IMapper mapper, IStringLocalizer<SharedResources> istringLocalizer)
        {
            this.studentServices = studentServices;
            this.mapper = mapper;
            this.istringLocalizer = istringLocalizer;
        }

        public async Task<Response<IEnumerable<GetAllStudentsResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var result = await studentServices.GetAllStudents();
            var mappedResult = mapper.Map<IEnumerable<GetAllStudentsResponse>>(result);
            return Success(mappedResult);
        }

        public async Task<Response<GetAllStudentsResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await studentServices.GetStudentById(request.Id);
            if (student == null)
                return NotFound<GetAllStudentsResponse>(istringLocalizer[SharedResourcesKeys.NotFound]);

            var result = mapper.Map<GetAllStudentsResponse>(student);
            return Success(result);
        }
        public async Task<PaginatedResult<GetAllStudentsPaginamtedResponse>> Handle(GetStudentPaginatedQuary request, CancellationToken cancellationToken)
        {

            Expression<Func<Student, GetAllStudentsPaginamtedResponse>> expression = e => new GetAllStudentsPaginamtedResponse(e.StudID, e.Address, e.Name, e.Department.DName);
            if (request.Search != null)
            {
                var Queryable = studentServices.GetStudentsQuarable(request.OrderBy, request.Search);
                var PaginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return PaginatedList;
            }
            else
            {
                var Queryable = studentServices.GetStudentsQuarable(request.OrderBy);
                var PaginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return PaginatedList;
            }

            // var filterQuary = studentServices.FilterStudentPaginatedQuerable(request.Search);
            //var PaginatiedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            //return PaginatiedList;
            //var PaginatedList = await mapper
            //.ProjectTo<GetStudentPaginatedListResponse>(FilterQuery)
            //.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            //PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            //return PaginatedList;
        }
    }
}
