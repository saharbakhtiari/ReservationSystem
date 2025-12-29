using System.Collections.Generic;

namespace CustomLoggers.AuditLog;

public interface IAuditLogBulkSet
{
    int Count { get; }

    void AddAuditToBulk(IAuditInfo auditInfo);

    List<IAuditInfo> ResetBulk();
}
