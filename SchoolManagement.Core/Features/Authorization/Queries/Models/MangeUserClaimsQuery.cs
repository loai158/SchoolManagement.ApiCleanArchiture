using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Queries.Response;

namespace SchoolManagement.Core.Features.Authorization.Queries.Models
{
    public class MangeUserClaimsQuery : IRequest<Response<ManageUserClaimsResponse>>
    {
        public string UserId { get; set; }


    }
}
