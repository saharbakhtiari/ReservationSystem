using System;

namespace CacheManagers.Contract;

public class RedisManagerException : Exception
{
    public RedisManagerException(int compCode, int reason, string message) : base(message)
    {
        CompCode = compCode;
        Reason = reason;
    }
    public RedisManagerException(int compCode, int reason, string message, Exception innerException) : base(message, innerException)
    {
        CompCode = compCode;
        Reason = reason;
    }
    public RedisManagerException(int compCode, int reason, int reasonCode, int completionCode, string message, Exception innerException) : base(message, innerException)
    {
        CompCode = compCode;
        Reason = reason;
        ReasonCode = reasonCode;
        CompletionCode = completionCode;
    }
    public RedisManagerException(string message) : base(message)
    {
    }
    protected RedisManagerException()
    {
    }

    public int Reason { get; set; }
    public int ReasonCode { get; set; }
    public int CompCode { get; set; }
    public int CompletionCode { get; set; }
}
