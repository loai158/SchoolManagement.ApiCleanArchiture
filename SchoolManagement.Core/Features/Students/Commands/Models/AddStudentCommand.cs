using MediatR;
using SchoolManagement.Core.Basics;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<String>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public String Phone { get; set; }
        [Required]

        public int DepatmentId { get; set; }
    }
}
