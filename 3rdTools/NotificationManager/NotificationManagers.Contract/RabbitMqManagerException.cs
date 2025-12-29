using System;

namespace NotificationManagers.Contract;

public class RabbitMqManagerException : Exception
{
    public RabbitMqManagerException(int compCode, int reason, string message) : base(message)
    {
        CompCode = compCode;
        Reason = reason;
    }
    public RabbitMqManagerException(int compCode, int reason, string message, Exception innerException) : base(message, innerException)
    {
        CompCode = compCode;
        Reason = reason;
    }
    public RabbitMqManagerException(int compCode, int reason, int reasonCode, int completionCode, string message, Exception innerException) : base(message, innerException)
    {
        CompCode = compCode;
        Reason = reason;
        ReasonCode = reasonCode;
        CompletionCode = completionCode;
    }
    public RabbitMqManagerException(string message) : base(message)
    {
    }
    protected RabbitMqManagerException()
    {
    }

    public int Reason { get; set; }
    public int ReasonCode { get; set; }
    public int CompCode { get; set; }
    public int CompletionCode { get; set; }
    public bool MQRC_NO_MSG_AVAILABLE => ReasonCode == 2033;
}
