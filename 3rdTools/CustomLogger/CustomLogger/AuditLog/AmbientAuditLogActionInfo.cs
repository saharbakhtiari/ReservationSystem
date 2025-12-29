using System.Threading;

namespace CustomLoggers.AuditLog;

public class AmbientAuditLogActionInfo : IAmbientAuditLogActionInfo
{
    public AuditLogActionInfo AuditLogActionInfo => _currentAuditLogActionInfo.Value;

    private readonly AsyncLocal<AuditLogActionInfo> _currentAuditLogActionInfo;

    public AmbientAuditLogActionInfo()
    {
        _currentAuditLogActionInfo = new AsyncLocal<AuditLogActionInfo>();
    }

    public void SetAuditLogActionInfo(AuditLogActionInfo auditLogActionInfo)
    {
        _currentAuditLogActionInfo.Value = auditLogActionInfo;
    }
}
