using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
