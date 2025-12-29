using System;

namespace Log4NetEmailAppender.LogEvent
{
    public interface IJsonSerializableExceptionFactory
    {
        JsonSerializableException Create(Exception exception);
    }
}