using Domain.Contract.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer.EmailNotifications
{
    public class CheckSmsNotify : Notification
    {
        public string PhoneNumber { get; set; }
        public string Key { get; set; }
        public Task<bool> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<bool>(NotifyServerApis.CheckSmsNotify, cancellationToken);
        }
    }
}
