using Domain.Contract.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer.EmailNotifications
{
    public class DeleteEmailNotify : Notification
    {
        public string EmailTo { get; set; }
        public string Key { get; set; }

        public Task<bool> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<bool>(NotifyServerApis.DeleteEmailNotify, cancellationToken);
        }
    }
}
