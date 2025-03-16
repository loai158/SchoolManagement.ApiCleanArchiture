using SchoolManagement.Data.Entities;
using SchoolManagement.Infrastructure.InfrastructureBases;

namespace SchoolManagement.Infrastructure.Abstracts
{
    public interface IStudentRepositry : IGenericRepositryAsync<Student>
    {
    }
}
