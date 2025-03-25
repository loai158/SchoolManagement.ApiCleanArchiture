using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.InfrastructureBases;

namespace SchoolManagement.Infrastructure.Repositries
{
    public class UserRefreshTokenRepositry : GenericRepositryAsync<UserRefreshToken>, IUserRefreshTokenRepositry
    {

        private readonly DbSet<UserRefreshToken> UserRefreshTokenRepo;

        public UserRefreshTokenRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.UserRefreshTokenRepo = dbContext.Set<UserRefreshToken>();
        }
    }
}
