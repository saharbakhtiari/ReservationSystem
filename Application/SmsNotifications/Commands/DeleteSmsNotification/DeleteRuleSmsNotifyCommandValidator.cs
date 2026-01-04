using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.SmsNotifications.Commands.DeleteSmsNotification
{
    public class DeleteRuleSmsNotifyCommandValidator : AbstractValidator<DeleteRuleSmsNotifyCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteRuleSmsNotifyCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.RuleId).GreaterThan(-1).WithMessage(_localizer["Rule Id is not valid"]);
        }
    }
}
