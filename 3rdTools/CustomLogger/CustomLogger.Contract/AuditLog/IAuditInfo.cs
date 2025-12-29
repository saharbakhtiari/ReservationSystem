using Extensions;
using System;
using System.Collections.Generic;

namespace CustomLoggers.AuditLog;

public interface IAuditInfo
{
    List<AuditLogActionInfo> Actions { get; set; }
    string ApplicationName { get; set; }
    string BrowserInfo { get; set; }
    string ClientId { get; set; }
    string ClientIpAddress { get; set; }
    string ClientName { get; set; }
    List<string> Comments { get; set; }
    Exception Exception { set; }
    string ExceptionText { get; }
    int ExecutionDuration { get; set; }
    DateTime ExecutionTime { get; set; }
    ExtraPropertyDictionary ExtraProperties { get; }
    string HttpMethod { get; set; }
    int? HttpStatusCode { get; set; }
    Guid? ImpersonatorUserId { get; set; }
    string OSVersion { get; set; }
    string Url { get; set; }
    Guid? UserId { get; set; }
    string UserName { get; set; }
    string ServerName { get; set; }
    string ServerIp { get; set; }
    string ServerOSVersion { get; set; }

    string ToString();
}

[Serializable]
public class AuditLogActionInfo
{
    public string ServiceName { get; set; }

    public string MethodName { get; set; }

    public string Parameters { get; set; }

    public DateTime ExecutionTime { get; set; }

    public int ExecutionDuration { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; }
    public Exception Exceptions { set; private get; }
    public string ExceptionsText
    {
        get
        {
            return Exceptions is null ? null : Exceptions.ToJson();
        }
    }

    public AuditLogActionInfo()
    {
        ExtraProperties = new ExtraPropertyDictionary();
    }
}

[Serializable]
public class ExtraPropertyDictionary : Dictionary<string, object>
{
    public ExtraPropertyDictionary()
    {

    }

    public ExtraPropertyDictionary(IDictionary<string, object> dictionary)
        : base(dictionary)
    {
    }
}