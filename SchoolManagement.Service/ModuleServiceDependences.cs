using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Data.Helper;
using SchoolManagement.Service.Abstacts;
using SchoolManagement.Service.Implementaions;
using System.Collections.Concurrent;

namespace SchoolManagement.Service
{
    public static class ModuleServiceDependences
    {
        public static IServiceCollection AddServiceDependences(this IServiceCollection services)
        {

            services.AddTransient<IStudentServices, StudentServices>();
            services.AddTransient<IDepartmentServices, DepartmentServices>();

            services.AddTransient<IAuthenticationServices, AuthenticationServices>();
            services.AddTransient<IApplicationUserServices, ApplicationUserServices>();

            services.AddTransient<IAuthorizationServices, AuthorizationServices>();
            services.AddTransient<IAuthenticationServices, AuthenticationServices>();
            services.AddTransient<IEmailServices, EmailServices>();
            services.AddSingleton<ConcurrentDictionary<string, RefreshToken>>();

            return services;
        }
    }
}
