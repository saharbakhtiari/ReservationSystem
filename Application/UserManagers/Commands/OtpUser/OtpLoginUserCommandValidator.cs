using FluentValidation;
namespace Application.UserManagers.Commands.OtpUser
{
    public class OtpLoginUserCommandValidator : AbstractValidator<OtpLoginUserCommand>
    {
        public OtpLoginUserCommandValidator()
        {
            RuleFor(v => v.PhoneNumber)
                .Length(11).WithMessage("شماره موبایل صحیح نیست")
                .Must(a => long.TryParse(a, out long x) == true).WithMessage("شماره موبایل صحیح نیست");
        }
    }
}