using MediatR;
using SchoolManagement.Core.Basics;

namespace SchoolManagement.Core.Features.Students.Commands.Models
{
    public class EditStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public String? Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}
