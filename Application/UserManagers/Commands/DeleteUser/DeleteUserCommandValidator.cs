using FluentValidation;

namespace Application.UserManagers.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithMessage("شناسه کاربر نمی تواند خالی باشد");
        }
    }
}
