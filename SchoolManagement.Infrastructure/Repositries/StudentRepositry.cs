using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.InfrastructureBases;

namespace SchoolManagement.Infrastructure.Repositries
{
    public class StudentRepositry : GenericRepositryAsync<Student>, IStudentRepositry
    {
        private readonly DbSet<Student> studentsRepo;

        public StudentRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.studentsRepo = dbContext.Set<Student>();
        }
    }
}
