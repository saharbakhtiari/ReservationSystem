using Domain.Contract.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer.EmailNotifications
{
    public class DeleteSourceEmail : Notification
    {
        public string UserName { get; set; }
        public Task<bool> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<bool>(NotifyServerApis.DeleteSourceEmail, cancellationToken);
        }
    }
}
