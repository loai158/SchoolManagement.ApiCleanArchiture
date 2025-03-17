using SchoolManagement.Core.Features.User.Queries.Responses;
using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetAllUsersPaginatedMapping()
        {
            CreateMap<ApplicationUser, GetUsersPaginatedResponse>();
        }
    }
}
