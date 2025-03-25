using FluentValidation;
using SchoolManagement.Core.Features.Authorization.Commands.Models;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Authorization.Commands.Validations
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        private readonly IAuthorizationServices authorizationServices;

        public AddRoleValidator(IAuthorizationServices authorizationServices)
        {
            ApplyValidationRules();
            ApplyCustomRules();
            this.authorizationServices = authorizationServices;
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.RoleName).NotEmpty()
                .NotNull().WithMessage("must has a value")
                .MaximumLength(20)
                .MinimumLength(5);


        }
        public void ApplyCustomRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (Key, CancellationToken) =>
                !await authorizationServices
                .IsRoleExistByName(Key)).WithMessage("Name Is Exist");



        }
    }
}
