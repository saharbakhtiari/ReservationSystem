using System.Threading.Tasks;

namespace CustomLoggers.AuditLog;

public class DefaultAuditlog : IAuditlogStorage
{
    public Task SaveAsync(IAuditInfo auditInfo)
    {
        return Task.CompletedTask;
    }
}

