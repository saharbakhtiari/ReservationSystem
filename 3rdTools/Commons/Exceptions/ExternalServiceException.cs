using System;
using System.Runtime.Serialization;

namespace Exceptions;

/// <summary>
/// Base exception type for those are thrown by system for End User specific exceptions.
/// </summary>
[Serializable]
public class ExternalServiceException : Exception
{
    /// <summary>
    /// Creates a new <see cref="ExternalServiceException"/> object.
    /// </summary>
    public ExternalServiceException()
    {

    }
    public int StatusCode { get; set; } 
    /// <summary>
    /// Creates a new <see cref="ExternalServiceException"/> object.
    /// </summary>
    public ExternalServiceException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    /// <summary>
    /// Creates a new <see cref="ExternalServiceException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public ExternalServiceException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="ExternalServiceException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public ExternalServiceException(string message, Exception innerException)
        : base(message, innerException)
    {

    }

    public ExternalServiceException(int statusCode,string message, Exception innerException)
       : base(message, innerException)
    {
        this.StatusCode = statusCode;
    }
}
