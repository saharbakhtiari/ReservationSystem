using FluentValidation;

namespace Application.UserManagers.Commands.EditUser
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            //RuleFor(v => v.Id).NotEmpty().WithMessage("شناسه کاربر نمی تواند خالی باشد");
            RuleFor(v => v.FirstName).NotEmpty().WithMessage("نام کاربر نمی تواند خالی باشد");
            RuleFor(v => v.LastName).NotEmpty().WithMessage("نام خانوادگی کاربر نمی تواند خالی باشد");
            //RuleFor(v => v.EmployeeNumber).NotEmpty().WithMessage("شماره پرسنلی کاربر نمی تواند خالی باشد");
        }
    }
}
