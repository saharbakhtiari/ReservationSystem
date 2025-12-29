using System.Threading.Tasks;

namespace CustomLoggers.AuditLog;

public class DefaultLoggerAuditlog : IAuditlogStorage
{
    private readonly ICustomLogger<DefaultLoggerAuditlog> _customLogger;
    public DefaultLoggerAuditlog(ICustomLogger<DefaultLoggerAuditlog> customLogger)
    {
        _customLogger = customLogger;
    }
    public Task SaveAsync(IAuditInfo auditInfo)
    {
        return _customLogger.LogInformation(auditInfo.ToString());
    }
}

