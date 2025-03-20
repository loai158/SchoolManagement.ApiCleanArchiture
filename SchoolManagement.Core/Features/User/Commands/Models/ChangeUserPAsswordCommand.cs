using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.User.Commands.Models
{
    public class ChangeUserPAsswordCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
