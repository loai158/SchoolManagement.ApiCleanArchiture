using AutoMapper;

namespace SchoolManagement.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetAllDepartmentMapping();
        }
    }
}
