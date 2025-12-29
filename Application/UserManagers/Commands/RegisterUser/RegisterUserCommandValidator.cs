using FluentValidation;

namespace Application.UserManagers.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(v => v.PhoneNumber)
                 .Length(11).WithMessage("شماره موبایل صحیح نیست")
                 .Must(a => long.TryParse(a, out long x) == true).WithMessage("شماره موبایل صحیح نیست");
        }
    }
}
