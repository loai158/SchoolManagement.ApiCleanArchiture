using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.InfrastructureBases;
using System.Linq.Expressions;

namespace SchoolManagement.Infrastructure.Abstracts
{
    public interface IDepartmentRepositry : IGenericRepositryAsync<Department>
    {
        public Task<Department?> GetOneSpecial(Expression<Func<Department, bool>> filter,
   List<Expression<Func<Department, object>>>? includes = null,
   List<Func<IQueryable<Department>, IQueryable<Department>>>? thenIncludes = null,
   bool tracked = true);

    }
}
