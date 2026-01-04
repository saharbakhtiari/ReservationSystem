using Application.Common.Security;
using MediatR;

namespace Application.Notifications.Commands.StartNotification;

[Authorize]
public class StarNotificationCommand : IRequest<bool>
{
    public bool BeStarted { get; set; }
}
