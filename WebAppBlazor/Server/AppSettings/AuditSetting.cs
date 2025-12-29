using System.Collections.Generic;

namespace WebAppBlazor.Server.AppSettings;

public class AuditSetting
{
    public List<string> ExceptionIP { get; set; }
    public List<string> ExceptionUrl { get; set; }
    public List<string> ExceptionHeader { get; set; }
}