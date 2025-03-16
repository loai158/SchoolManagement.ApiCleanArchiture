using SchoolManagement.Data.Entities;
using SchoolManagement.Data.Helper;

namespace SchoolManagement.Service.Abstacts
{
    public interface IStudentServices
    {
        public Task<IEnumerable<Student>> GetAllStudents();

        public Task<Student> GetStudentById(int id);
        public Task<String> AddStudentAsync(Student student);

        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
        public string Edit(Student student);

        public Task<string> Delete(int id);
        public IQueryable<Student> GetStudentsQuarable(StudentOrderingEnum order, string search);
        public IQueryable<Student> GetStudentsByDeptQuarable(int DID);
        public IQueryable<Student> GetStudentsQuarable(StudentOrderingEnum order);
    }
}
