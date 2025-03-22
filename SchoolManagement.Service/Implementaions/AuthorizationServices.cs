using Microsoft.AspNetCore.Identity;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Service.Implementaions
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthorizationServices(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }
        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
