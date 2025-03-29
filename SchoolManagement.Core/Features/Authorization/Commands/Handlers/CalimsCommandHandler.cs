using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Commands.Models;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Infrastructure.UnitOfWorks;
using System.Security.Claims;

namespace SchoolManagement.Core.Features.Authorization.Commands.Handlers
{
    public class CalimsCommandHandler : ResponseHandler,
        IRequestHandler<UpadteUserClaimCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CalimsCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        public async Task<Response<string>> Handle(UpadteUserClaimCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return NotFound<string>("User Not Found");
                }
                else
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    var RemovedClaims = _userManager.RemoveClaimsAsync(user, userClaims);
                    if (RemovedClaims.IsCompletedSuccessfully)
                    {
                        var claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                        var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
                        if (!addUserClaimResult.Succeeded)
                            return BadRequest<string>("fiald to add new claim");
                        await _unitOfWork.CommitTransactionAsync();
                        return Success<string>("success");
                    }

                    return BadRequest<string>("Some thing went wrong");
                }
            }
            catch (Exception)
            {

                await _unitOfWork.RollbackTransactionAsync();
                return NotFound<string>("TRANSACTION FAILD");
            }
        }
    }
}
