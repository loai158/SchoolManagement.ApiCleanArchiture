using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.UnitOfWorks;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Service.Implementaions
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepositry departmentRepositry;

        public DepartmentServices(IUnitOfWork unitOfWork, IDepartmentRepositry departmentRepositry)
        {
            this._unitOfWork = unitOfWork;
            this.departmentRepositry = departmentRepositry;
        }
        public async Task<Department> GetDepartmentById(int id)
        {

            var department = await departmentRepositry
                .GetOneSpecial(d => d.DID == id,
                 includes: [d => d.DepartmentSubjects, di => di.Instructors, i => i.Instructor],
                 thenIncludes: [q => q.Include(d => d.DepartmentSubjects).ThenInclude(ds => ds.Subject)], // ثم تضمين المادة الخاصة بالقسم
                 tracked: false);
            return department;
        }

        public async Task<bool> IsDepartmentExist(int id)
        {
            var DepartmentDb = await _unitOfWork.Repositry<Department>().GetOne(s => s.DID == id);
            if (DepartmentDb == null)
            {
                return false;
            }
            return true;
        }
    }
}
