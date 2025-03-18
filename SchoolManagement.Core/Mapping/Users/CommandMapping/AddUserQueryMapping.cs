using SchoolManagement.Core.Features.User.Commands.Models;
using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void AddUserQueryMapping()
        {
            CreateMap<AddUserCommand, ApplicationUser>();


        }
    }
}
