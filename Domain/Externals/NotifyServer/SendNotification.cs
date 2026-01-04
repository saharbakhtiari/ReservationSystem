using Domain.Contract.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer
{
    public class SendNotification : Notification
    {
        /// <summary>
        /// Source email username
        /// </summary>
        public string SenderEmailUserName { get; set; }
        public List<string> Keys { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SenderNumber { get; set; }
        public SendNotification()
        {
            Keys = new List<string>();
        }

        public Task<bool> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<bool>(NotifyServerApis.SendNotification, cancellationToken);
        }
    }
}
