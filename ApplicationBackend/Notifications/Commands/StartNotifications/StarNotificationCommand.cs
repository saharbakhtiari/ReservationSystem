using Application.Notifications.Commands.StartNotification;
using Domain.Externals.NotifyServer.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Notifications.Commands.StartNotifications;

public class StarNotificationCommandHandler : IRequestHandler<StarNotificationCommand, bool>
{

    public async Task<bool> Handle(StarNotificationCommand request, CancellationToken cancellationToken)
    {
        if (request.BeStarted)
        {
            await Start(cancellationToken);
            return true;
        }
        await Stop(cancellationToken);
        return true;
    }

    private async Task Start(CancellationToken cancellationToken)
    {
        StartNotification start = new()
        {
        };
        await start.SendAsync(cancellationToken);
    }

    private async Task Stop(CancellationToken cancellationToken)
    {
        StopNotification stop = new()
        {
        };
        await stop.SendAsync(cancellationToken);
    }
}
