using CustomLoggers;
using Domain.Common;
using Domain.Common.Interfaces;
using MediatR;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ICustomLogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserManager _accountService;

        public PerformanceBehaviour(
            ICustomLogger<TRequest> logger
            //ICurrentUserService currentUserService,
            //IUserManager accountService
            )
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUserService = ServiceLocator.ServiceProvider.GetService<ICurrentUserService>();//currentUserService;
            _accountService = ServiceLocator.ServiceProvider.GetService<IUserManager>(); //accountService;
        }

        public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            var userName = string.Empty;

            userName = _currentUserService.UserName;
            if (string.IsNullOrWhiteSpace(userName))
            {
                if (userId.HasValue && userId != Guid.Empty)
                {
                    userName = await _accountService.GetUserNameAsync(userId.GetValueOrDefault());
                }
            }

            _ = _logger.LogInformation("request performance: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);

            if (elapsedMilliseconds > 400)
            {
                _ = _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                    requestName, elapsedMilliseconds, userId, userName, request);
            }

            return response;
        }
    }
}
