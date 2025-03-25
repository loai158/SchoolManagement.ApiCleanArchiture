using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Service.Abstacts;
using SchoolManagement.Service.Implementaions;

namespace SchoolManagement.Service
{
    public static class ModuleServiceDependences
    {
        public static IServiceCollection AddServiceDependences(this IServiceCollection services)
        {

            services.AddTransient<IStudentServices, StudentServices>();
            services.AddTransient<IDepartmentServices, DepartmentServices>();

            services.AddTransient<IAuthenticationServices, AuthenticationServices>();

            services.AddTransient<IAuthorizationServices, AuthorizationServices>();

            services.AddTransient<IAuthenticationServices, AuthenticationServices>();

            return services;
        }
    }
}
