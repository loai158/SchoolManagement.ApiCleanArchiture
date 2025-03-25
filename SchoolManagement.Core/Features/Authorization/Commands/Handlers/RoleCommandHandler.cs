using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Commands.Models;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Infrastructure.UnitOfWorks;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>

    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationServices _authorizationServices;

        public RoleCommandHandler(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IAuthorizationServices authorizationServices)
        {
            this._roleManager = roleManager;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._authorizationServices = authorizationServices;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.AddRoleAsync(request.RoleName);
            if (result == "Success") return Success("");
            return BadRequest<string>("Faild To Add the Role");
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var roleInDb = await _roleManager.FindByIdAsync(request.Id);
            if (roleInDb != null)
            {
                roleInDb.Name = request.Name;
                var result = await _roleManager.UpdateAsync(roleInDb);
                if (result.Succeeded)
                {
                    return Success<string>("edited successfully");

                }
                else
                {
                    return BadRequest<string>("");
                }

            }
            else
            {
                return NotFound<string>("Role is not found");
            }
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var roleInDb = await _roleManager.FindByIdAsync(request.Id);
            if (roleInDb != null)
            {
                var result = await _roleManager.DeleteAsync(roleInDb);
                if (result.Succeeded)
                {
                    return Success<string>("Deleted successfully");

                }
                else
                {
                    return BadRequest<string>("");
                }

            }
            else
            {
                return NotFound<string>("Role is not found");
            }
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, roles);

                    if (removeResult.Succeeded)
                    {

                        var selectedRoles = request.Roles.Where(x => x.HasRole == true).Select(x => x.Name).ToString();
                        var result = await _userManager.AddToRoleAsync(user, selectedRoles);
                        await _unitOfWork.CommitTransactionAsync();
                        if (result.Succeeded)
                        {
                            return Success<string>("");
                        }
                        else
                        {
                            return BadRequest<string>("failed To Add To Roles");
                        }
                    }
                    else
                    {
                        return BadRequest<string>("Failed To Remove the Roles");
                    }
                }
                else
                {
                    return NotFound<string>("Role is not found");
                }
            }
            catch (Exception)
            {

                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest<string>("FailedToUpdateUserRoles");
            }

        }

    }
}
