using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Students.Queries.Responses;

namespace SchoolManagement.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Response<GetAllStudentsResponse>>
    {
        public int Id { get; set; }

        public GetStudentByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
