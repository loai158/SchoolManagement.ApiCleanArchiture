using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Service.Abstacts
{
    public interface IApplicationUserServices
    {
        public Task<string> AddUserAsync(ApplicationUser user, string password);
    }
}
