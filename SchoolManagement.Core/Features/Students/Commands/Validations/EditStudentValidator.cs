using FluentValidation;
using SchoolManagement.Core.Features.Students.Commands.Models;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentServices studentServices;

        public EditStudentValidator(IStudentServices studentServices)
        {
            ApplyValidationRules();
            ApplyCustomRules();
            this.studentServices = studentServices;
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name).NotEmpty()
                .NotNull()
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
                .MustAsync(async (Model, Key, CancellationToken) => !await studentServices
                .IsNameExistExcludeSelf(Key, Model.Id)).WithMessage("Name Is Exist");


        }
    }
}
