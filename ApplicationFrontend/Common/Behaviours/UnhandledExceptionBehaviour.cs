using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IAuthService _authService;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {

                var user = await _authService.GetCurrentUserAsync();

                var requestName = typeof(TRequest).Name;

                var userName = user.FindFirst(ClaimTypes.Name)?.Value;

                string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                _logger.LogWarning(ex, "Request: Unhandled Exception for Request {Name} - {@Request} : by {@UserName} {@UserId}", requestName, request, userName, userId);

                throw;
            }
        }
    }
}
