using SchoolManagement.Core.Features.Students.Queries.Responses;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetAllStudentsMapping()
        {
            CreateMap<Student, GetAllStudentsResponse>()
               .ForMember(dest => dest.DeparmentName, opt => opt.MapFrom(src => src.Department.DName));
        }
    }
}
