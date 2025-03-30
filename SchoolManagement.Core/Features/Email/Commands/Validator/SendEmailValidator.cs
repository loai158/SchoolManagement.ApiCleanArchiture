using FluentValidation;
using SchoolManagement.Core.Features.Email.Commands.Models;

namespace SchoolManagement.Core.Features.Email.Commands.Validator
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {

        public SendEmailValidator()
        {

            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Must Have A value")
                 .NotNull().WithMessage("is null");

            RuleFor(x => x.Message)
                 .NotEmpty().WithMessage("Must Have A value")
                 .NotNull().WithMessage("is null");
        }
    }
}
