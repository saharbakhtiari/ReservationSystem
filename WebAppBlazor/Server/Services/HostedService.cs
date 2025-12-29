using CustomLoggers;
using Domain.Common;
using Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Services
{
    public class Runner : IHostedService
    {
        private readonly CancellationTokenSource _stopTokenSource = new CancellationTokenSource();

        private Task _backgroundJobManagerTask;

        private readonly ICustomLogger<Runner> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _scopedServiceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IBackgroundJobManager _backgroundJobManager;
        public Runner(IServiceProvider serviceProvider, ICustomLogger<Runner> logger, IConfiguration configuration, ILoggerFactory loggerFactory, IBackgroundJobManager backgroundJobManager)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _backgroundJobManager = backgroundJobManager;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            try
            {
                _loggerFactory.AddLog4Net();
                if (_configuration.GetValue<bool>("AllowBackgroundJob").Not())
                {
                    return Task.CompletedTask;
                }
                _scopedServiceProvider = _serviceProvider.CreateScope();
                ServiceLocator.Initialize(_serviceProvider.GetService<IServiceProviderProxy>());
                ServiceLocator.ServiceProvider.HostedServiceServiceProvider = _scopedServiceProvider.ServiceProvider;

                _ = _logger.LogWarning($"{nameof(Runner)} started");

                var jobServices = _serviceProvider.GetServices<IJobService>();

                _backgroundJobManagerTask = _backgroundJobManager.RunAsync(jobServices, _stopTokenSource.Token);

            }
            catch (Exception ex)
            {
                _ = _logger.LogWarning(ex, $"{nameof(Runner)} StartAsync has error");
                throw;
            }

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _ = _logger.LogWarning($"{ nameof(Runner)} stoped");
            _stopTokenSource.Cancel();
            await _backgroundJobManagerTask;
        }
    }
}
