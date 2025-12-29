using System.Threading.Tasks;

namespace CustomLoggers.AuditLog;

public interface IAuditlogStorage
{
    Task SaveAsync(IAuditInfo auditInfo);
}

