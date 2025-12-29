using Log4NetEmailAppender.MailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Log4NetEmailAppender.Bulk
{
    public class LogBulkSet : ILogBulkSet
    {
        private List<InnerBulkOperation> _bulk = new List<InnerBulkOperation>();

        public int Count
        {
            get
            {
                lock (_bulk)
                {
                    return _bulk.Count;
                }
            }
        }

        public void AddEventToBulk(Dictionary<string, object> logEvent)
        {
            var operationParamValues = logEvent.ToDictionary(
                param => param.Key,
                param => param.Value.ToString());

            var operation = new InnerBulkOperation
            {
                OperationParams = operationParamValues
            };

            lock (_bulk)
            {
                _bulk.Add(operation);
            }
        }

        public List<InnerBulkOperation> ResetBulk()
        {
            var currentBulk = Interlocked.Exchange(ref _bulk, new List<InnerBulkOperation>());
            return currentBulk;
        }



    }
}
