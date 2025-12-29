namespace CustomLoggers.AuditLog;

public class ElasticSearchConfiguration : IElasticSearchConfiguration
{

    /// <summary>
    /// 授权用户名
    /// </summary>
    public string AuthUserName { get; set; }

    /// <summary>
    /// 授权密码
    /// </summary>
    public string AuthPassWord { get; set; }

    /// <summary>
    /// 是否保存审计日志到Es
    /// </summary>
    public bool UseAuditingLog { get; set; }

    public int BulkSize { get; set; }
    public int BulkIdleTimeout { get; set; }

    public string IndexType { get; set; } = "_doc";

    public string SaveProvider { get; set; }

    public bool SSL { get; set; }
    public bool AllowSelfSignedServerCert { get; set; }
    public string Address { get; set; }
    public string Path { get; set; }
    public int Port { get; set; }
    public int Timeout { get; set; }

    /// <summary>
    /// 审计日志索引名称，默认值：Sed-audit-log
    /// </summary>
    public string AuditingLogIndexName { get; set; } = "Samtak-audit-log";
    public string CertPath { get; set; }
    public string CertPass { get; set; }
}

