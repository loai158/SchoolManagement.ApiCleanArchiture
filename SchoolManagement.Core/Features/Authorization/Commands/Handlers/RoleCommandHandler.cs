using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Commands.Models;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthorizationServices _authorizationServices;

        public RoleCommandHandler(RoleManager<IdentityRole> roleManager, IAuthorizationServices authorizationServices)
        {
            this._roleManager = roleManager;
            this._authorizationServices = authorizationServices;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.AddRoleAsync(request.RoleName);
            if (result == "Success") return Success("");
            return BadRequest<string>("Faild To Add the Role");
        }
    }
}
