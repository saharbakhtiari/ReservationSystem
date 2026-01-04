using Application.Common.Security;
using MediatR;

namespace Application.SmsNotifications.Commands.DeleteSmsNotification;

[Authorize]
public class DeleteRuleSmsNotifyCommand : IRequest<bool>
{
    public long RuleId { get; set; }
}
