using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.Email.Commands.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }


    }
}
