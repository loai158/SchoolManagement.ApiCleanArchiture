using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Data.Helper;

namespace SchoolManagement.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
