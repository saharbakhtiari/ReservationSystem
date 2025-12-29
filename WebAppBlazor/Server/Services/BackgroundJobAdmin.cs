using CustomLoggers;
using Domain.Common;
using Domain.Settings;
using Domain.UnitOfWork.Uow;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Services
{
    public class BackgroundJobAdmin : IJobServiceAdmin
    {
        private readonly string serviceName = nameof(BackgroundJobAdmin);

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICustomLogger<BackgroundJobAdmin> _logger;

        public BackgroundJobAdmin(IUnitOfWorkManager unitOfWorkManager,
            ICustomLogger<BackgroundJobAdmin> logger)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _logger = logger;
        }

        public async Task RunAsync(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                _ = _logger.LogWarning($"{serviceName} job started run");
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true }))
                        {
                            try
                            {
                                var activeService = await Setting.GetSettingByKeyAsync(SettingKeys.BackgroundJobActiveService, cancellationTokenSource.Token);
                                if (activeService is null)
                                {
                                    activeService = new()
                                    {
                                        Key = SettingKeys.BackgroundJobActiveService,
                                        Value = Environment.MachineName
                                    };
                                }
                                else
                                {
                                    activeService.LastModifiedUtc = DateTime.UtcNow;
                                    activeService.Value = Environment.MachineName;
                                }

                                await activeService.SaveAsync(cancellationTokenSource.Token);
                                await uow.CompleteAsync(cancellationTokenSource.Token);
                            }
                            catch (Exception ex)
                            {
                                _ = _logger.LogWarning(ex, $"{serviceName}  Raised Error: {ex.Message}");

                                cancellationTokenSource.Cancel();
                            }
                        }
                        await Task.Delay(TimeSpan.FromSeconds(30), cancellationTokenSource.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _ = _logger.LogError(ex, $"{serviceName} Raise error : {ex.Message}");
                    }
                }
                _ = _logger.LogWarning($"{serviceName} job finished run");
            }
            catch (Exception ex)
            {
                _ = _logger.LogWarning(ex, $"{serviceName} is stopped");
            }
        }
    }

}
