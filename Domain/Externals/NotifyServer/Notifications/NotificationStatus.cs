using Domain.Contract.Common;
using Domain.Contract.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer.Notifications
{
    public class NotificationStatus : Notification
    {
        public Task<MqStatus> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<MqStatus>(NotifyServerApis.GetNotificationStatus, cancellationToken);
        }
    }
}
