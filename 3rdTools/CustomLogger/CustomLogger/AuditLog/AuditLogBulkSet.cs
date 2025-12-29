using System.Collections.Generic;
using System.Threading;

namespace CustomLoggers.AuditLog;

public class AuditLogBulkSet : IAuditLogBulkSet
{
    private List<IAuditInfo> _bulk = new List<IAuditInfo>();

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

    public void AddAuditToBulk(IAuditInfo auditInfo)
    {
        lock (_bulk)
        {
            _bulk.Add(auditInfo);
        }
    }

    public List<IAuditInfo> ResetBulk()
    {
        var currentBulk = Interlocked.Exchange(ref _bulk, new List<IAuditInfo>());
        return currentBulk;
    }

}
