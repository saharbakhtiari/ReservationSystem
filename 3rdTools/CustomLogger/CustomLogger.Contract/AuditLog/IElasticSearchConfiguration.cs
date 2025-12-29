namespace CustomLoggers.AuditLog;

public interface IElasticSearchConfiguration
{
    /// <summary>
    /// 授权用户名
    /// </summary>
    string AuthUserName { get; set; }

    /// <summary>
    /// 授权密码
    /// </summary>
    string AuthPassWord { get; set; }

    /// <summary>
    /// 是否保存审计日志到Es
    /// </summary>
    bool UseAuditingLog { get; set; }
    bool AllowSelfSignedServerCert { get; set; }
    int BulkSize { get; set; }
    int BulkIdleTimeout { get; set; }

    string SaveProvider { get; set; }

    string IndexType { get; set; }
    int Timeout { get; set; }

    bool SSL { get; set; }
    string Address { get; set; }
    string Path { get; set; }
    int Port { get; set; }

    /// <summary>
    /// 审计日志索引名称
    /// </summary>
    string AuditingLogIndexName { get; set; }
    string CertPath { get; set; }
    string CertPass { get; set; }
}

