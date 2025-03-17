using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.User.Queries.Responses;

namespace SchoolManagement.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public string Id { get; set; }

        public GetUserByIdQuery(string id)
        {
            this.Id = id;
        }

    }
}
