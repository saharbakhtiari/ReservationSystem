using Application.Notifications.Queries.GetNotificationStatus;
using Domain.Contract.Enums;
using Domain.Externals.NotifyServer.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Notifications.Queries.GetNotificationStatus;

public class StarNotificationQueryHandler : IRequestHandler<GetNotificationStatusQuery, MqStatus>
{

    public Task<MqStatus> Handle(GetNotificationStatusQuery request, CancellationToken cancellationToken)
    {
        NotificationStatus status = new()
        {
        };
        return status.SendAsync(cancellationToken);
    }
}
