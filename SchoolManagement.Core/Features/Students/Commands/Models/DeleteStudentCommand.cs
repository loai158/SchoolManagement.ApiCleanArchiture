using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<Response<string>>
    {
        public readonly int Id;

        public DeleteStudentCommand(int id)
        {
            this.Id = id;
        }
    }
}
