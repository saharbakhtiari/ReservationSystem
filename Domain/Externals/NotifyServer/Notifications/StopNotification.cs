using Domain.Contract.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer.Notifications
{
    public class StopNotification : Notification
    {
        public Task<bool> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<bool>(NotifyServerApis.StopNotification, cancellationToken);
        }
    }
}
