
using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }

    }
}
