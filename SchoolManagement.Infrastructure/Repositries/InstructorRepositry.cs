using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.InfrastructureBases;

namespace SchoolManagement.Infrastructure.Repositries
{
    public class InstructorRepositry : GenericRepositryAsync<Instructor>, IInstructorRepositry
    {
        private readonly DbSet<Instructor> instructorRepo;

        public InstructorRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.instructorRepo = dbContext.Set<Instructor>();
        }
    }
}
