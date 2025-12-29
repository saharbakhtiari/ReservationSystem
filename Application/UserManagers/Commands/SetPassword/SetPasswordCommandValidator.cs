using FluentValidation;

namespace Application.UserManagers.Commands.SetPassword
{
    public class SetPasswordCommandValidator : AbstractValidator<SetPasswordCommand>
    {
        public SetPasswordCommandValidator()
        {
            RuleFor(v => v.Password).NotEmpty().WithMessage("کلمه عبور نمیتواند خالی باشد");
        }
    }
}
