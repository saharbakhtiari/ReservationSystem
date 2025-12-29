using FluentValidation;

namespace Application.UserManagers.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(v => v.CurrentPassword).NotEmpty().WithMessage("کلمه عبور جاری نمیتواند خالی باشد");
            RuleFor(v => v.NewPassword).NotEmpty().WithMessage("کلمه عبور جدید نمیتواند خالی باشد");
            RuleFor(v => v.NewPassword).Equal(y => y.ConfirmPassword).WithMessage("کلمه عبور جدید همخوانی ندارد ");
        }
    }
}
