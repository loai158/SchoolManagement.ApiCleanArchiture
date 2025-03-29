using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Queries.Models;
using SchoolManagement.Core.Features.Authorization.Queries.Response;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Data.Helper;

namespace SchoolManagement.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<MangeUserClaimsQuery, Response<ManageUserClaimsResponse>>

    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimsQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<Response<ManageUserClaimsResponse>> Handle(MangeUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var response = new ManageUserClaimsResponse();
            var claimList = new List<UserClaims>();
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound<ManageUserClaimsResponse>(" User Not Found ");
            }
            else
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var claims = ClaimStore.claims;
                response.UserId = user.Id;
                foreach (var claim in claims)
                {
                    var userClaim = new UserClaims();
                    userClaim.Type = claim.Type;
                    if (userClaims.Any(x => x.Type == claim.Type))
                    {
                        userClaim.Value = true;
                    }
                    else
                    {
                        userClaim.Value = false;
                    }
                    claimList.Add(userClaim);

                }
                response.userClaims = claimList;
                return Success(response);
            }
        }
    }
}
