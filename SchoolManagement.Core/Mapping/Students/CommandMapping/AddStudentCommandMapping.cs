using SchoolManagement.Core.Features.Students.Commands.Models;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudentCommandMapping()
        {
            CreateMap<AddStudentCommand, Student>()
               .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepatmentId));
        }
    }
}
