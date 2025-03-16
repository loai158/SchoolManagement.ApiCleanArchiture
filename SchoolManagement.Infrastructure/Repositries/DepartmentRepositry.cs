using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.InfrastructureBases;
using System.Linq.Expressions;

namespace SchoolManagement.Infrastructure.Repositries
{
    public class DepartmentRepositry : GenericRepositryAsync<Department>, IDepartmentRepositry
    {
        private readonly DbSet<Department> departmentRepo;

        public DepartmentRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.departmentRepo = dbContext.Set<Department>();
        }

        public async Task<Department?> GetOneSpecial(Expression<Func<Department, bool>> filter,
    List<Expression<Func<Department, object>>>? includes = null,
    List<Func<IQueryable<Department>, IQueryable<Department>>>? thenIncludes = null,
    bool tracked = true)
        {
            IQueryable<Department> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (thenIncludes != null)
            {
                foreach (var thenInclude in thenIncludes)
                {
                    query = thenInclude(query);  // تطبيق ThenInclude بشكل ديناميكي
                }
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
