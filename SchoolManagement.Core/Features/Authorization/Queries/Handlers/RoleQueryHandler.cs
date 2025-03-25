using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Authorization.Queries.Models;
using SchoolManagement.Core.Features.Authorization.Queries.Response;
using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetAllRolesQuery, Response<List<GetAllRolesResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetAllRolesResponse>>,
        IRequestHandler<MangeUserRolesQuery, Response<MangeUserRolesResponse>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleQueryHandler(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<Response<List<GetAllRolesResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.ToList();
            var mapped = _mapper.Map<List<GetAllRolesResponse>>(roles);
            return Success(mapped);
        }

        public async Task<Response<GetAllRolesResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return NotFound<GetAllRolesResponse>("Role is not found");
            }
            var mapped = _mapper.Map<GetAllRolesResponse>(role);
            return Success(mapped);
        }

        public async Task<Response<MangeUserRolesResponse>> Handle(MangeUserRolesQuery request, CancellationToken cancellationToken)
        {
            var response = new MangeUserRolesResponse();
            var RolesList = new List<UserRoles>();
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound<MangeUserRolesResponse>(" User Not Found ");
            }
            else
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles;
                response.UserId = user.Id;
                foreach (var role in roles)
                {
                    var userrole = new UserRoles();
                    userrole.Id = role.Id;
                    userrole.Name = role.Name;
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userrole.HasRole = true;
                    }
                    else
                    {
                        userrole.HasRole = false;
                    }
                    RolesList.Add(userrole);

                }
                response.Roles = RolesList;
                return Success(response);

            }
        }
    }
}

