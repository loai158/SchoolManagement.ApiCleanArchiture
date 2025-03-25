namespace SchoolManagement.Service.Abstacts
{
    public interface IAuthorizationServices
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);

    }
}
