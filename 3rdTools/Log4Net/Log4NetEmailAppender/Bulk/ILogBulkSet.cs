using Log4NetEmailAppender.MailSender;
using System.Collections.Generic;

namespace Log4NetEmailAppender.Bulk
{
    public interface ILogBulkSet
    {
        int Count { get; }

        void AddEventToBulk(Dictionary<string, object> logEvent);

        List<InnerBulkOperation> ResetBulk();
    }
}