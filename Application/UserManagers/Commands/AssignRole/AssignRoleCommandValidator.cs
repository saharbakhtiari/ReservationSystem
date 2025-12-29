using FluentValidation;

namespace Application.UserManagers.Commands.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(v => v.UserId).NotEmpty().WithMessage("ابتدا کاربر را انتخاب نمایید");
            RuleFor(v => v.Roles).NotNull().WithMessage("لیست نقش ها نا معتبر می باشد");
        }
    }
}
