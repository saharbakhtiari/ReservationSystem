using Extensions;
using log4stash.Timing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomLoggers.AuditLog;

public class ElasticAuditlog : IAuditlogStorage, IDisposable
{
    private readonly IElasticSearchConfiguration _elasticSearchConfiguration;
    private readonly ISaveInElastic _elasticSaver;
    private readonly IServiceProvider _serviceProvider;

    private readonly ICustomLogger<ElasticAuditlog> _logger;
    private readonly IIndexingTimer _timer;
    private readonly IAuditLogBulkSet _bulk;

    public ElasticAuditlog(IElasticSearchConfiguration elasticSearchConfiguration,
        IAuditLogBulkSet bulk,
        ICustomLogger<ElasticAuditlog> logger,
        IServiceProvider serviceProvider)
    {
        _elasticSearchConfiguration = elasticSearchConfiguration;
        _serviceProvider = serviceProvider;

        _elasticSaver = _serviceProvider.GetServiceWithName<ISaveInElastic>(_elasticSearchConfiguration.SaveProvider);

        _bulk = bulk;
        _logger = logger;

        _timer = new IndexingTimer(_elasticSearchConfiguration.BulkIdleTimeout) { WaitTimeout = 5000 };
        _timer.Elapsed += (o, e) => DoIndexNow();
    }


    public Task SaveAsync(IAuditInfo auditInfo)
    {
        try
        {
            _bulk.AddAuditToBulk(auditInfo);

            if (_bulk.Count >= _elasticSearchConfiguration.BulkSize)
            {
                DoIndexNow();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return Task.CompletedTask;
    }

    private void DoIndexNow()
    {
        var bulkToSend = _bulk.ResetBulk();
        if (bulkToSend != null && bulkToSend.Count <= 0) return;

        try
        {
            _ = SaveAsync(bulkToSend);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    private async Task SaveAsync(List<IAuditInfo> auditInfos)
    {
        try
        {
            if (_elasticSaver is not null)
            {
                await _elasticSaver.SaveAsync(auditInfos);
            }
            else
            {
                _ = _logger.LogWarning("elasticSaver is null");
            }
        }
        catch (Exception ex)
        {
            _ = _logger.LogError(ex, ex.Message);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DoIndexNow();

            // let the timer finish its job
            _timer.Dispose();
        }

    }
}


