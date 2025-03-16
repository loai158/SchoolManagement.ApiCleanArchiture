using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Students.Queries.Responses;
namespace SchoolManagement.Core.Features.Students.Queries.Models
{
    public class GetAllStudentsQuery : IRequest<Response<IEnumerable<GetAllStudentsResponse>>>
    {
    }
}
