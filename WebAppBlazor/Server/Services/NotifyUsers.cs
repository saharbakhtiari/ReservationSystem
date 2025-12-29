using Domain.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using WebAppBlazor.Server.Hubs;

namespace WebAppBlazor.Server.Services
{
    //public class NotifyUsers : INotifyUsers
    //{
    //    private readonly IHubContext<NotifyHub> _hubContext;
    //    private readonly IMediator _mediator;
    //    private readonly IPushNotificationService _pushNotificationService;

    //    public NotifyUsers(IHubContext<NotifyHub> hubContext, IMediator mediator, IPushNotificationService pushNotificationService)
    //    {
    //        _hubContext = hubContext;
    //        _mediator = mediator;
    //        _pushNotificationService = pushNotificationService;
    //    }

    //    public async Task NotifyUserAsync(Guid userId, NotifyType notifyType, object extraData)
    //    {
    //        await _hubContext.Clients.User(userId.ToString()).SendAsync(Messages.RECEIVE, notifyType, extraData);
    //        if (notifyType == NotifyType.Notification)
    //        {
    //            var subscriptions = await _mediator.SendWithUow(new GetUserSubscriptionQuery { UserId = userId });
    //            foreach (var item in subscriptions)
    //            {
    //                // purposefully not awaiting. Fire and forget.
    //                _pushNotificationService.SendNotificationAsync(item, new PushMessage(extraData.ToString())
    //                {
    //                    Urgency = PushMessageUrgency.Normal
    //                });

    //            }
    //        }
    //    }

    //    public async Task NotifyUserListAsync(List<Guid> userIds, NotifyType notifyType, object extraData)
    //    {
    //        await _hubContext.Clients.Users(userIds.Select(x => x.ToString()).ToList()).SendAsync(Messages.RECEIVE, notifyType, extraData);
    //    }
    //}
}
