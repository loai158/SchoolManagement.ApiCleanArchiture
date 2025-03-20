using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.User.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public DeleteUserCommand(string id)
        {
            this.Id = id;
        }
    }
}
