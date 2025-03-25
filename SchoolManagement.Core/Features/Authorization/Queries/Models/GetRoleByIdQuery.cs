using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Queries.Response;

namespace SchoolManagement.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetAllRolesResponse>>
    {
        public string Id { get; set; }
        public GetRoleByIdQuery(string id)
        {

            Id = id;
        }
    }
}
