using System;
using System.Runtime.Serialization;

namespace Exceptions;

/// <summary>
/// Base exception type for those are thrown by system for End User specific exceptions.
/// </summary>
[Serializable]
public class UserFriendlyException : Exception
{
    /// <summary>
    /// Creates a new <see cref="UserFriendlyException"/> object.
    /// </summary>
    public UserFriendlyException()
    {

    }

    /// <summary>
    /// Creates a new <see cref="UserFriendlyException"/> object.
    /// </summary>
    public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    /// <summary>
    /// Creates a new <see cref="UserFriendlyException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public UserFriendlyException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="UserFriendlyException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public UserFriendlyException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
