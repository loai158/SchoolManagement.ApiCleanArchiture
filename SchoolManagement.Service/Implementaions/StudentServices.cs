using SchoolManagement.Data.Entities;
using SchoolManagement.Data.Helper;
using SchoolManagement.Infrastructure.Abstracts;
using SchoolManagement.Infrastructure.UnitOfWorks;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Service.Implementaions
{

    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepositry studentRepositry;
        private readonly IUnitOfWork unitOfWork;

        public StudentServices(IStudentRepositry studentRepositry, IUnitOfWork unitOfWork)
        {
            this.studentRepositry = studentRepositry;
            this.unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await studentRepositry.Get(includes: [d => d.Department]);
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await unitOfWork.Repositry<Student>().GetOne(filter: s => s.StudID == id, includes: [d => d.Department], tracked: false);
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            var result = await studentRepositry.Create(student);
            studentRepositry.Commit();
            return result;
        }

        public async Task<bool> IsNameExist(string name)
        {
            var studentDb = await studentRepositry.GetOne(s => s.Name == name);
            if (studentDb == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var studentDb = await studentRepositry.GetOne(s => s.Name == name && s.StudID != id);
            if (studentDb == null)
            {
                return false;
            }
            return true;
        }

        public string Edit(Student student)
        {
            studentRepositry.Edit(student);
            studentRepositry.Commit();
            return "success";
        }
        public async Task<string> Delete(int id)
        {
            var trans = unitOfWork.BeginTransactionAsync();
            try
            {
                var student = await unitOfWork.Repositry<Student>().GetOne(s => s.StudID == id);
                unitOfWork.Repositry<Student>().Delete(student);
                unitOfWork.Repositry<Student>().Commit();
                await unitOfWork.CommitTransactionAsync();
                return "success";
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                return "faild";

            }
        }

        public IQueryable<Student> GetStudentsQuarable(StudentOrderingEnum order, string search)
        {
            var query = studentRepositry.GetQuarable(includes: [d => d.Department], tracked: true)
                .Where(s => (s.Name.Contains(search)) || (s.Address.Contains(search)));
            switch (order)
            {
                case StudentOrderingEnum.StudID:
                    query.OrderBy(s => s.StudID);
                    break;
                case StudentOrderingEnum.Address:
                    query.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.Name:
                    query.OrderBy(s => s.Name);
                    break;
                case StudentOrderingEnum.DeparmentName:
                    query.OrderBy(s => s.Department.DName);
                    break;

            }

            return query;

        }
        public IQueryable<Student> GetStudentsQuarable(StudentOrderingEnum order)
        {

            var query = studentRepositry.GetQuarable(includes: [d => d.Department], tracked: true);
            switch (order)
            {
                case StudentOrderingEnum.StudID:
                    query.OrderBy(s => s.StudID);
                    break;
                case StudentOrderingEnum.Address:
                    query.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.Name:
                    query.OrderBy(s => s.Name);
                    break;
                case StudentOrderingEnum.DeparmentName:
                    query.OrderBy(s => s.Department.DName);
                    break;

            }
            return query;
        }

        public IQueryable<Student> GetStudentsByDeptQuarable(int DID)
        {
            var students = unitOfWork.Repositry<Student>().GetQuarable(filter: s => s.DID == DID);
            return students;
        }

    }
}
