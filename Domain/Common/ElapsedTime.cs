using CustomLoggers;
using CustomLoggers.AuditLog;
using Domain.Common.Interfaces;
using Domain.UnitOfWork;
using Extensions;
using System;

namespace Domain.Common;


public interface IElapsedTime<T> : IDisposable
{
    string Message { get; set; }
    TimeSpan ThresholdElapsedTime { get; set; }
}

public class ElapsedTime<T> : IElapsedTime<T>
{
    public string Message { get; set; }
    public TimeSpan ThresholdElapsedTime { get; set; }

    private DateTime startDateTime;
    private readonly IAuditInfo _auditInfo;
    private readonly ICustomLogger<ElapsedTime<T>> _logger;
    private readonly IRequestOrginService _requestOrgin;

    public ElapsedTime(IAuditInfo auditInfo, ICustomLogger<ElapsedTime<T>> logger, IRequestOrginService requestOrgin)
    {
        startDateTime = DateTime.UtcNow;
        _auditInfo = auditInfo;
        _logger = logger;
        _requestOrgin = requestOrgin;
    }
    private bool _isDisposed = false;
    public void Dispose()
    {
        if (_isDisposed.Not())
        {
            _isDisposed = true;
            Dispose(true);
        }
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            var endDateTime = DateTime.UtcNow;

            var elapsedTime = endDateTime - startDateTime;

            if (_requestOrgin.IsFromClient)
            {
                _auditInfo.Actions.Add(new AuditLogActionInfo()
                {
                    ExecutionTime = startDateTime.ToLocalTime(),
                    ExecutionDuration = (int)elapsedTime.TotalMilliseconds,
                    ServiceName = nameof(ElapsedTime<T>),
                    Parameters = Message,
                    MethodName = typeof(T).Name,
                });
            }

            if (elapsedTime > ThresholdElapsedTime)
            {
                _logger.LogWarning(message: $"{Message} : ElapsedTime = {elapsedTime}");
            }
            else
            {
                _logger.LogInformation(message: $"{Message} : ElapsedTime = {elapsedTime}");
            }
        }
    }
}

public interface IElapsedTimeManager
{
    IElapsedTime<T> Begin<T>(TimeSpan ThresholdElapsedTime, string Message = "");
}

public class ElapsedTimeManager : IElapsedTimeManager
{
    public ElapsedTimeManager()
    {
    }

    public IElapsedTime<T> Begin<T>(TimeSpan ThresholdElapsedTime, string Message = "")
    {
        Check.NotNull(ThresholdElapsedTime, nameof(ThresholdElapsedTime));

        var elapsedTime = ServiceLocator.ServiceProvider.GetService<IElapsedTime<T>>();

        elapsedTime.Message = Message;
        elapsedTime.ThresholdElapsedTime = ThresholdElapsedTime;

        return elapsedTime;
    }
}