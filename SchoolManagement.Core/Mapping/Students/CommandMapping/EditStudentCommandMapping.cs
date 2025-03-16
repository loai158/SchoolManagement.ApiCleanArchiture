using AutoMapper;
using SchoolManagement.Core.Features.Students.Commands.Models;
using SchoolManagement.Data.Entities;
namespace SchoolManagement.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
               .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId)).
               ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
