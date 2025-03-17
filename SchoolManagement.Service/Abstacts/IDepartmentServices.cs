using SchoolManagement.Data.Entities;

namespace SchoolManagement.Service.Abstacts
{
    public interface IDepartmentServices
    {
        public Task<Department> GetDepartmentById(int id);
        public Task<bool> IsDepartmentExist(int id);
    }
}
