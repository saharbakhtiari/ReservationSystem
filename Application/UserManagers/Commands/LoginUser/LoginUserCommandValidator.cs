using FluentValidation;
namespace Application.UserManagers.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(v => v.UserName).NotEmpty().WithMessage("نام کاربری نمی تواند خالی باشد");
            RuleFor(v => v.Password).NotEmpty().WithMessage("کلمه عبور نمی تواند خالی باشد");
        }
    }
}