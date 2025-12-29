using FluentValidation;

namespace Application.UserManagers.Commands.VerifyOtpLogin
{
    public class VerifyOtpLoginCommandValidator : AbstractValidator<VerifyOtpLoginCommand>
    {
        public VerifyOtpLoginCommandValidator()
        {
            RuleFor(v => v.PhoneNumber)
                .Length(11).WithMessage("شماره موبایل صحیح نیست")
                .Must(a => long.TryParse(a, out long x) == true).WithMessage("شماره موبایل صحیح نیست");
            RuleFor(v => v.VerifyCode).NotEmpty().WithMessage("کد وارد نشده است");
        }
    }
}
