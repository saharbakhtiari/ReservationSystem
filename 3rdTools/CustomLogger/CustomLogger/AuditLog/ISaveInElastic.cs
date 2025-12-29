using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomLoggers.AuditLog;

public interface ISaveInElastic
{
    Task SaveAsync(List<IAuditInfo> auditInfos);
}
