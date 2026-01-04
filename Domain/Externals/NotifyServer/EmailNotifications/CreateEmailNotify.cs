using Domain.Contract.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer.EmailNotifications
{
    public class CreateEmailNotify : Notification
    {
        public string EmailTo { get; set; }
        public string Key { get; set; }

        public Task<long> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<long>(NotifyServerApis.CreateEmailNotify, cancellationToken);
        }
    }
}
