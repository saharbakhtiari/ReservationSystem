using FluentValidation;

namespace Application.UserManagers.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //RuleFor(v => v.UserName).NotEmpty().WithMessage("Cannot be empty");
            //RuleFor(v => v.Password).NotEmpty().WithMessage("Cannot be empty");
            RuleFor(v => v.PhoneNumber)
                .Length(11).WithMessage("شماره موبایل صحیح نیست")
                .Must(a => long.TryParse(a, out long x) == true).WithMessage("شماره موبایل صحیح نیست");
        }
    }
}
