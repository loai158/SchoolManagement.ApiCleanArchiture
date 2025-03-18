using AutoMapper;

namespace SchoolManagement.Core.Mapping.Users
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserQueryMapping();
            GetAllUsersPaginatedMapping();
            GetUserByIdMapping();
            EditUserCommandMapping();
        }
    }
}
