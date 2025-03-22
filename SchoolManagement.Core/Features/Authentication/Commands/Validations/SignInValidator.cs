using FluentValidation;
using SchoolManagement.Core.Features.Authentication.Commands.Models;

namespace SchoolManagement.Core.Features.Authentication.Commands.Validations
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("must has a value");


            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                 .WithMessage("must has a value");

        }

    }
}
