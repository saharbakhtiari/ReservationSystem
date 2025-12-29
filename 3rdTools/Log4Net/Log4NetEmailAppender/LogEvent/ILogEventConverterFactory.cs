using log4net.Core;

namespace Log4NetEmailAppender.LogEvent
{
    public interface ILogEventConverterFactory
    {
        ILogEventConverter Create(FixFlags fixedFields, bool serializeObjects);
    }
}