namespace CustomLoggers.AuditLog;

public interface IAmbientAuditLogActionInfo
{
    AuditLogActionInfo AuditLogActionInfo { get; }

    void SetAuditLogActionInfo(AuditLogActionInfo auditLogActionInfo);
}
