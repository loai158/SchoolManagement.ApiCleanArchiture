using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public DeleteRoleCommand(string id)
        {
            this.Id = id;
        }
    }
}
