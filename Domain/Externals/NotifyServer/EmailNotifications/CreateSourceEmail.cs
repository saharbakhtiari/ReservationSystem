using Domain.Contract.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer.EmailNotifications
{
    public class CreateSourceEmail : Notification
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailServer { get; set; }
        public string Domain { get; set; }
        public int Port { get; set; }
        public Task<long> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<long>(NotifyServerApis.CreateSourceEmail, cancellationToken);
        }
    }
}
