namespace SchoolManagement.Core.Features.Authorization.Queries.Response
{
    public class MangeUserRolesResponse
    {
        public string UserId { get; set; }
        public List<UserRoles> Roles { get; set; }
    }
    public class UserRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; } = false;
    }
}
