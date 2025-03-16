using FluentValidation;
using SchoolManagement.Core.Features.Students.Commands.Models;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentServices studentServices;

        public AddStudentValidator(IStudentServices studentServices)
        {
            ApplyValidationRules();
            ApplyCustomRules();
            this.studentServices = studentServices;
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
        }
        public void ApplyCustomRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) =>
                !await studentServices
                .IsNameExist(Key)).WithMessage("Name Is Exist");
        }
    }
}
