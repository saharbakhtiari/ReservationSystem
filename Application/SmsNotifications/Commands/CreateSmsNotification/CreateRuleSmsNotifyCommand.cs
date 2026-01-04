using Application.Common.Security;
using Domain.Enums;
using MediatR;

namespace Application.SmsNotifications.Commands.CreateSmsNotification;

[Authorize]
public class CreateRuleSmsNotifyCommand : IRequest<bool>
{
    public long RuleId { get; set; }
}
