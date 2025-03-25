using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.InfrastructureBases;

namespace SchoolManagement.Infrastructure.Abstracts
{
    public interface IUserRefreshTokenRepositry : IGenericRepositryAsync<UserRefreshToken>
    {
    }
}
