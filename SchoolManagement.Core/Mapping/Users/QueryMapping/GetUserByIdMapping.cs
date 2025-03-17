using SchoolManagement.Core.Features.User.Queries.Responses;
using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<ApplicationUser, GetUserByIdResponse>();
        }
    }

}
