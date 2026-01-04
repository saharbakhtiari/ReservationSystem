using CustomLoggers;
using Domain.Common;
using Domain.Common.Interfaces;
using Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICustomLogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;

        public UnhandledExceptionBehaviour(ICustomLogger<TRequest> logger/*, ICurrentUserService currentUserService*/)
        {
            _logger = logger;
            _currentUserService = ServiceLocator.ServiceProvider.GetService<ICurrentUserService>();//currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (UserFriendlyException ex)
            {
                var requestName = typeof(TRequest).Name;

                var userName = _currentUserService.UserName;

                string userId = _currentUserService.UserId?.ToString();

                _ = _logger.LogInformation(ex, "Request: UserFriendlyException for Request {Name} - {@Request} : by {@UserName} {@UserId}", requestName, request, userName, userId);

                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                var userName = _currentUserService.UserName;

                string userId = _currentUserService.UserId?.ToString();

                _ = _logger.LogError(ex, "Request: Unhandled Exception for Request {Name} - {@Request} : by {@UserName} {@UserId}", requestName, request, userName, userId);

                throw;
            }
        }
    }
}
