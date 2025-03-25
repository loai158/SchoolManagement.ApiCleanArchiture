using FluentValidation;
using SchoolManagement.Core.Features.Authorization.Commands.Models;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Authorization.Commands.Validations
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        private readonly IAuthorizationServices authorizationServices;

        public EditRoleValidator(IAuthorizationServices authorizationServices)
        {
            ApplyValidationRules();

            this.authorizationServices = authorizationServices;
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id).NotEmpty()
                .NotNull().WithMessage("must has a value");

            RuleFor(x => x.Name).NotEmpty()
                .NotNull().WithMessage("must has a value")
                .MaximumLength(20)
                .MinimumLength(5);
        }
    }
}
