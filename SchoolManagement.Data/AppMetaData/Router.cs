namespace SchoolManagement.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "v1";
        public const string Rule = root + "/" + version + "/";
        public static class StudentRouting
        {
            public const string Perfix = Rule + "Student";
            public const string List = Perfix + "/List";
            public const string GetById = Perfix + "/{id}";
            public const string Create = Perfix + "/Create";
            public const string Edit = Perfix + "/Edit";
            public const string Delete = Perfix + "/{id}";
            public const string Paginate = Perfix + "/Paginate";
        }
        public static class DepartmentRouting
        {
            public const string Perfix = Rule + "Department";
            public const string List = Perfix + "/List";
            public const string GetById = Perfix + "/Id";
            public const string Create = Perfix + "/Create";
            public const string Edit = Perfix + "/Edit";
            public const string Delete = Perfix + "/{id}";
            public const string Paginate = Perfix + "/Paginate";
        }
        public static class ApplicationUserRouting
        {
            public const string Perfix = Rule + "ApplicationUser";
            public const string List = Perfix + "/List";
            public const string GetById = Perfix + "/{id}";
            public const string Register = Perfix + "/Register";
            public const string Edit = Perfix + "/Edit";
            public const string Delete = Perfix + "/{id}";
            public const string Paginate = Perfix + "/Paginate";
            public const string ChangePassword = Perfix + "/ChangePassword";
        }
        public static class Authentication
        {
            public const string Perfix = Rule + "Authentication";
            public const string SignIn = Perfix + "/SignIn";
            public const string RefreshToken = Perfix + "/RefreshToken";

        }
        public static class EmailsRoute
        {
            public const string Perfix = Rule + "EmailsRoute";
            public const string SendEmail = Perfix + "/SendEmail";
        }

        public static class Authorization
        {
            public const string Perfix = Rule + "Authorization";
            public const string Claims = Perfix + "/Claims";
            public const string AddRole = Perfix + "/AddRole";
            public const string Edit = Perfix + "/Edit";
            public const string Delete = Perfix + "/{id}";
            public const string GetAll = Perfix + "/List";
            public const string GetById = Perfix + "/{id}";
            public const string ManageUserRoles = Perfix + "/Manage-The-Roles/{userId}";
            public const string UpdateUserRoles = Perfix + "/Update-User-Roles";
            public const string ManageUserClaims = Perfix + "/Manage-User-Claims/{userId}";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";
        }
    }
}
