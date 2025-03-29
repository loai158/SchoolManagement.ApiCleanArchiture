using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.Authorization.Commands.Models
{
    public class UpadteUserClaimCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public List<UserClaims> userClaims { get; set; }

        public class UserClaims
        {
            public string Type { get; set; }

            public bool Value { get; set; }
        }
    }
}
