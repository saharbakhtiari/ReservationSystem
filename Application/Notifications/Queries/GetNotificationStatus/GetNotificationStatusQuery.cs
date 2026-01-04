using Application.Common.Security;
using Domain.Contract.Enums;
using MediatR;

namespace Application.Notifications.Queries.GetNotificationStatus;

 [Authorize]
public class GetNotificationStatusQuery : IRequest<MqStatus>
{
}
