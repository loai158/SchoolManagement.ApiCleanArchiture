using FluentValidation;
using SchoolManagement.Core.Features.Students.Commands.Models;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentServices studentServices;
        private readonly IDepartmentServices departmentServices;

        public AddStudentValidator(IStudentServices studentServices, IDepartmentServices departmentServices)
        {
            ApplyValidationRules();
            ApplyCustomRules();
            this.studentServices = studentServices;
            this.departmentServices = departmentServices;
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name).NotEmpty()
                .NotNull().WithMessage("must has a value")
                .MaximumLength(20)
                .MinimumLength(5);
            RuleFor(x => x.Address).NotEmpty()
              .NotNull()
              .MaximumLength(20)
              .MinimumLength(5);

            RuleFor(x => x.DepatmentId).NotEmpty().WithMessage("must have a department")
              .NotNull().WithMessage("shoud not be null");

        }
        public void ApplyCustomRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) =>
                !await studentServices
                .IsNameExist(Key)).WithMessage("Name Is Exist");

            When(p => p.DepatmentId != null, () =>
            {
                RuleFor(x => x.DepatmentId)
                    .MustAsync(async (Key, CancellationToken) =>
                    await departmentServices.IsDepartmentExist(Key))
                    .WithMessage("Department Is Not Exist");
            });

        }
    }
}
