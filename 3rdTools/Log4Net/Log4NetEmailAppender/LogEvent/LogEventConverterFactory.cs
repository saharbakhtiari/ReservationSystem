using log4net.Core;

namespace Log4NetEmailAppender.LogEvent
{
    public class BasicLogEventConverterFactory : ILogEventConverterFactory
    {
        public ILogEventConverter Create(FixFlags fixedFields, bool serializeObjects)
        {
            return new BasicLogEventConverter(fixedFields, serializeObjects);
        }
    }
}
