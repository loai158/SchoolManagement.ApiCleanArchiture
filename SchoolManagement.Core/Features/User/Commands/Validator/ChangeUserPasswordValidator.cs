using FluentValidation;
using SchoolManagement.Core.Features.User.Commands.Models;

namespace SchoolManagement.Core.Features.User.Commands.Validator
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPAsswordCommand>
    {
        public ChangeUserPasswordValidator()
        {

            ApplyValidationRules();
        }



        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull().WithMessage("must has a value");


            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .NotNull()
                .Equal(x => x.NewPassword)
                .WithMessage("Must Match the NewPassword");




        }
    }
}
