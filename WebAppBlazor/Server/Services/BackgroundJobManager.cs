using CustomLoggers;
using Domain.Common;
using Domain.Settings;
using Domain.UnitOfWork.Uow;
using Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Services
{
    public class BackgroundJobManager : IBackgroundJobManager
    {
        private CancellationTokenSource _stopTokenSource = new();

        private readonly string serviceName = nameof(BackgroundJobManager);

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICustomLogger<BackgroundJobManager> _logger;
        private readonly IJobServiceAdmin _jobServiceAdmin;

        private List<Task> _queueTask;

        public BackgroundJobManager(IUnitOfWorkManager unitOfWorkManager, 
            ICustomLogger<BackgroundJobManager> logger, 
            IJobServiceAdmin jobServiceAdmin)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _logger = logger;
            _jobServiceAdmin = jobServiceAdmin;
        }

        public async Task RunAsync(IEnumerable<IJobService> jobServices, CancellationToken cancellationToken)
        {
            try
            {
                _ = _logger.LogWarning($" {serviceName} job started run");
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true }))
                        {
                            try
                            {
                                var activeService = await Setting.GetSettingByKeyAsync(SettingKeys.BackgroundJobActiveService, cancellationToken);
                                if (activeService is null || activeService.LastModifiedUtc.HasValue.Not() || (DateTime.UtcNow - activeService.LastModifiedUtc.Value) > TimeSpan.FromMinutes(1))
                                {
                                    _stopTokenSource = new CancellationTokenSource();

                                    if (activeService is null)
                                    {
                                        activeService = new()
                                        {
                                            Key = SettingKeys.BackgroundJobActiveService,
                                            Value = Environment.MachineName,
                                            LastModifiedUtc = DateTime.UtcNow
                                    };
                                    }
                                    else
                                    {
                                        activeService.LastModifiedUtc = DateTime.UtcNow;
                                        activeService.Value = Environment.MachineName;
                                    }

                                    await activeService.SaveAsync(cancellationToken);
                                    await uow.CompleteAsync(cancellationToken);

                                    _queueTask = new List<Task>
                                    {
                                        _jobServiceAdmin.RunAsync(_stopTokenSource)
                                    };
                                    foreach (var item in jobServices)
                                    {
                                        _queueTask.Add(item.RunAsync(_stopTokenSource.Token));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _ = _logger.LogWarning(ex, $" {serviceName}  Raised Error: {ex.Message}");
                            }
                        }
                        await Task.Delay(TimeSpan.FromSeconds(30),cancellationToken);
                    }
                    catch (TaskCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _ = _logger.LogError(ex, $" {serviceName}   Raise error : {ex.Message}");
                    }

                }
                _ = _logger.LogWarning($"{serviceName} job finished run ");
            }
            catch (Exception ex)
            {
                _ = _logger.LogWarning(ex, $" {serviceName} is stopped");
            }
            _stopTokenSource.Cancel();
            await Task.WhenAll(_queueTask);
        }
    }

}
