using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Queries.Response;

namespace SchoolManagement.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : MangeUserRolesResponse, IRequest<Response<string>>
    {
    }
}
