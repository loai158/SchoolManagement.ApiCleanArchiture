using AutoMapper;

namespace SchoolManagement.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetAllStudentsMapping();
            AddStudentCommandMapping();
            EditStudentCommandMapping();
        }
    }
}
