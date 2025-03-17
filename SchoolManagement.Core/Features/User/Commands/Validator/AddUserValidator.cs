using FluentValidation;
using SchoolManagement.Core.Features.User.Commands.Models;

namespace SchoolManagement.Core.Features.User.Commands.Validator
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            ApplyCustomRules();
            ApplyValidationRules();
        }



        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName).NotEmpty()
                .NotNull().WithMessage("must has a value")
                .MaximumLength(30)
                .MinimumLength(3);

            RuleFor(x => x.Address)
              .MaximumLength(30)
              .MinimumLength(5);

            RuleFor(x => x.UserName).NotEmpty()
                .NotNull()
              .MaximumLength(20)
              .MinimumLength(5);

            RuleFor(x => x.Password).NotEmpty()
                .NotNull()
              .MaximumLength(20)
              .MinimumLength(5);
            RuleFor(x => x.Email).NotEmpty().NotNull()
              .MaximumLength(20)
              .MinimumLength(5);
            RuleFor(x => x.ConfirmPassword).NotEmpty()
               .NotNull().Matches(x => x.Password).WithMessage("password and confirmpassword are not matched")
             .MaximumLength(20)
             .MinimumLength(5);



        }
        public void ApplyCustomRules()
        {
        }
    }
}
