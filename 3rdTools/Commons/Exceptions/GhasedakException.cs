using System;
using System.Runtime.Serialization;

namespace Exceptions;

/// <summary>
/// Base exception type for those are thrown by system for End User specific exceptions.
/// </summary>
[Serializable]
public class GhasedakException : Exception
{
    /// <summary>
    /// Creates a new <see cref="GhasedakException"/> object.
    /// </summary>
    public GhasedakException()
    {

    }

    /// <summary>
    /// Creates a new <see cref="GhasedakException"/> object.
    /// </summary>
    public GhasedakException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    /// <summary>
    /// Creates a new <see cref="GhasedakException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public GhasedakException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="GhasedakException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public GhasedakException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
