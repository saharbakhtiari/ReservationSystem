using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.UserManagers.Commands.EditPhoneNumber
{
    public class EditPhoneNumberCommandValidator : AbstractValidator<EditPhoneNumberCommand>
    {
        private readonly IStringLocalizer _localizer;
        public EditPhoneNumberCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            //RuleFor(v => v.Id).NotEmpty().WithMessage("شناسه کاربر نمی تواند خالی باشد");
            RuleFor(v => v.PhoneNumber).NotEmpty().WithMessage(_localizer["Phone number is empty"]);
            RuleFor(v => v.VerifyCode).NotEmpty().WithMessage(_localizer["Otp code is empty"]);
            //RuleFor(v => v.EmployeeNumber).NotEmpty().WithMessage("شماره پرسنلی کاربر نمی تواند خالی باشد");
        }
    }
}
