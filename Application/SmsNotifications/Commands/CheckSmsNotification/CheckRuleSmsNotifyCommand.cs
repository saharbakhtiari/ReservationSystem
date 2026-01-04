using Application.Common.Security;
using MediatR;

namespace Application.SmsNotifications.Commands.CheckSmsNotification;

[Authorize]
public class CheckRuleSmsNotifyCommand : IRequest<bool>
{
    public long RuleId { get; set; }
}
