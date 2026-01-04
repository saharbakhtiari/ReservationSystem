using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.SmsNotifications.Commands.CheckSmsNotification
{
    public class CheckRuleSmsNotifyCommandValidator : AbstractValidator<CheckRuleSmsNotifyCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CheckRuleSmsNotifyCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.RuleId).GreaterThan(-1).WithMessage(_localizer["Rule Id is not valid"]);
        }
    }
}
