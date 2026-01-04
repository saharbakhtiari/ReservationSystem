using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.SmsNotifications.Commands.CreateSmsNotification
{
    public class CreateRuleSmsNotifyCommandValidator : AbstractValidator<CreateRuleSmsNotifyCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateRuleSmsNotifyCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.RuleId).GreaterThan(-1).WithMessage(_localizer["Rule Id is not valid"]);
        }
    }
}
