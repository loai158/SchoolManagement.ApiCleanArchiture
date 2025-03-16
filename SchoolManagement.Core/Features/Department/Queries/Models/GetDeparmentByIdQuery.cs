using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Department.Queries.Responses;

namespace SchoolManagement.Core.Features.Department.Queries.Models
{
    public class GetDeparmentByIdQuery : IRequest<Response<GetAllDepartmentResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }

    }
}
