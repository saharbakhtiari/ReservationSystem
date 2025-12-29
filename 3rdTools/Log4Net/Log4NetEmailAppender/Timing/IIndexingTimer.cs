using System;

namespace Log4NetEmailAppender.Timing
{
    public interface IIndexingTimer : IDisposable
    {
        event EventHandler Elapsed;
        void ElapsedAction();
        void Restart(int timeout);
    }
}