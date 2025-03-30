using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Data.Helper;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.InfrastructureBases;
using SchoolManagement.Infrastructure.Repositries;
using SchoolManagement.Infrastructure.UnitOfWorks;

namespace SchoolManagement.Infrastructure
{
    public static class ModuleInfrastructureDependences
    {
        public static IServiceCollection AddInfrastructureDependences(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepositry, StudentRepositry>();
            services.AddTransient<IDepartmentRepositry, DepartmentRepositry>();
            services.AddTransient<IInstructorRepositry, InstructorRepositry>();
            services.AddTransient<ISubjectRepositry, SubjectRepositry>();
            services.AddTransient<IUserRefreshTokenRepositry, UserRefreshTokenRepositry>();
            services.AddTransient<EmailSettings, EmailSettings>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IGenericRepositryAsync<>), typeof(GenericRepositryAsync<>));
            return services;
        }
    }
}
