using System;
using System.Runtime.Serialization;

namespace Domain.UnitOfWork
{
    /// <summary>
    /// Base exception type for those are thrown by Sed system for Sed specific exceptions.
    /// </summary>
    [Serializable]
    public class SedException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="SedException"/> object.
        /// </summary>
        public SedException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="SedException"/> object.
        /// </summary>
        public SedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SedException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public SedException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SedException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public SedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
