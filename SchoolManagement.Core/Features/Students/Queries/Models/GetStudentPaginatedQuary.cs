using MediatR;
using SchoolManagement.Core.Features.Students.Queries.Responses;
using SchoolManagement.Core.Wrapper;
using SchoolManagement.Data.Helper;

namespace SchoolManagement.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedQuary : IRequest<PaginatedResult<GetAllStudentsPaginamtedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
