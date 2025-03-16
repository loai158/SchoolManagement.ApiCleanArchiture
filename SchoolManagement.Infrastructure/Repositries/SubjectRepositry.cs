using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.InfrastructureBases;

namespace SchoolManagement.Infrastructure.Repositries
{
    public class SubjectRepositry : GenericRepositryAsync<Subject>, ISubjectRepositry
    {
        private readonly DbSet<Subject> subjectRepo;

        public SubjectRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.subjectRepo = dbContext.Set<Subject>();
        }
    }
}
