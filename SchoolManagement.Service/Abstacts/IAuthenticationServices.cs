using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Data.Helper;
namespace SchoolManagement.Service.Abstacts
{
    public interface IAuthenticationServices
    {
        //  public Task<string> RegisterAsync(ApplicationUser applicationUser);
        public Task<JwtAuthResult> CreateJwtToken(ApplicationUser user);

    }
}
