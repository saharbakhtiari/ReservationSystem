using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomLoggers.AuditLog;

[Serializable]
public class AuditInfo : IAuditInfo
{
    public string ServerName { get; set; }
    public string ServerIp { get; set; }
    public string ServerOSVersion { get; set; }

    public string ApplicationName { get; set; }

    public Guid? UserId { get; set; }

    public string UserName { get; set; }

    public Guid? ImpersonatorUserId { get; set; }

    public string ImpersonatorUserName { get; set; }

    public DateTime ExecutionTime { get; set; }

    public int ExecutionDuration { get; set; }

    public string ClientId { get; set; }

    public string ClientIpAddress { get; set; }

    public string ClientName { get; set; }

    public string BrowserInfo { get; set; }

    public string HttpMethod { get; set; }

    public int? HttpStatusCode { get; set; }

    public string Url { get; set; }

    /// <summary>
    /// Version of the OS.
    /// </summary>
    public string OSVersion { get; set; }

    public List<AuditLogActionInfo> Actions { get; set; }

    public Exception Exception { private get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; }

    public List<string> Comments { get; set; }

    public string ExceptionText => Exception is null ? null : Exception.ToJson();

    public AuditInfo()
    {
        Actions = new List<AuditLogActionInfo>();
        ExtraProperties = new ExtraPropertyDictionary();
        Comments = new List<string>();

        try
        {
            ServerName = Environment.MachineName;
            ServerOSVersion = Environment.OSVersion.VersionString;
        }
        catch
        {
        }

    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"AUDIT LOG: [{HttpStatusCode?.ToString() ?? "---"}: {(HttpMethod ?? "-------").PadRight(7)}] {Url}");
        sb.AppendLine($"- UserName - UserId                 : {UserName} - {UserId}");
        sb.AppendLine($"- ClientIpAddress        : {ClientIpAddress}");
        sb.AppendLine($"- ExecutionDuration      : {ExecutionDuration}");

        if (Actions.Any())
        {
            sb.AppendLine("- Actions:");
            foreach (var action in Actions)
            {
                sb.AppendLine($"  - {action.ServiceName}.{action.MethodName} ({action.ExecutionDuration} ms.)");
                sb.AppendLine($"    {action.Parameters}");
            }
        }

        if (Exception is not null)
        {
            sb.AppendLine("- Exceptions:");

            sb.AppendLine($"  - {Exception.Message}");
            sb.AppendLine($"    {ExceptionText}");

        }

        return sb.ToString();
    }

}
