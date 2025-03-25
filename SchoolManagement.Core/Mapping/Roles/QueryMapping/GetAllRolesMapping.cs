using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Features.Authorization.Queries.Response;

namespace SchoolManagement.Core.Mapping.Roles
{
    public partial class RolesProfile
    {
        public void GetAllRolesMapping()
        {
            CreateMap<IdentityRole, GetAllRolesResponse>();
        }
    }

}
