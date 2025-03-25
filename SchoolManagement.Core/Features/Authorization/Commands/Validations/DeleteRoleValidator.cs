using FluentValidation;
using SchoolManagement.Core.Features.Authorization.Commands.Models;

namespace SchoolManagement.Core.Features.Authorization.Commands.Validations
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {

        public DeleteRoleValidator()
        {
            ApplyValidationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id).NotEmpty()
                .NotNull().WithMessage("must has a value");
        }
    }
}
