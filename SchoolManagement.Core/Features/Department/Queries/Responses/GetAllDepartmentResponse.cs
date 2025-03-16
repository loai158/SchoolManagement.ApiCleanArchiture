using SchoolManagement.Core.Wrapper;

namespace SchoolManagement.Core.Features.Department.Queries.Responses
{
    public class GetAllDepartmentResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Manger { get; set; }
        public List<InstructorList>? Instructors { get; set; }
        public PaginatedResult<StudentList>? Students { get; set; }
        public List<SubjectList>? Subjects { get; set; }
    }
    public class StudentList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentList(int id, string Name)
        {
            this.Id = id;
            this.Name = Name;
        }

    }
    public class InstructorList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
