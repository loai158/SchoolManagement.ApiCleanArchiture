using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authentication.Commands.Responses;

namespace SchoolManagement.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<SignInResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
