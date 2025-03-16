using SchoolManagement.Core.Features.Department.Queries.Responses;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetAllDepartmentMapping()
        {
            CreateMap<Department, GetAllDepartmentResponse>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DName))
               .ForMember(dest => dest.Manger, opt => opt.MapFrom(src => src.InsManger))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
               .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors))
               //  .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
               .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.DepartmentSubjects));
            CreateMap<DepartmetSubject, SubjectList>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.SubjectName));
            CreateMap<Instructor, InstructorList>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            //CreateMap<Student, StudentList>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }

    }
}
