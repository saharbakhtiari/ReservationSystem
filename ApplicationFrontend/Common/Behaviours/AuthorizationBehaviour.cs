using Application.Common.Security;
using Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IAuthService _authService;

        public AuthorizationBehaviour(
            ILogger<TRequest> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var user = await _authService.GetCurrentUserAsync();
            Guid? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value.To<Guid>();

            var authorizeAttributes = request.GetType()
                .GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (userId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var authorizeAttributesWithAccess = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles) || !string.IsNullOrWhiteSpace(a.Permissions));

                if (authorizeAttributesWithAccess.Any())
                {
                    foreach (var authorizeAttribute in authorizeAttributesWithAccess)
                    {
                        var authorized = false;

                        if (authorizeAttribute.Roles.IsNullOrWhiteSpace().Not())
                        {
                            var roles = authorizeAttribute.Roles.Split(',');
                            foreach (var role in roles)
                            {
                                var isInRole = user.IsInRole(role.Trim());
                                if (isInRole)
                                {
                                    authorized = true;
                                    continue;
                                }
                            }
                        }
                        if (authorized.Not() && authorizeAttribute.Permissions.IsNullOrWhiteSpace().Not())
                        {
                            var permissions = authorizeAttribute.Permissions.Split(',');
                            foreach (var permission in permissions)
                            {
                                var hasPermission = HasPermission(user, permission.Trim());
                                if (hasPermission)
                                {
                                    authorized = true;
                                    continue;
                                }
                            }
                        }

                        // Must be a member of at least one role in roles
                        if (!authorized)
                        {
                            throw new UnauthorizedAccessException();
                        }
                    }
                }
            }

            // User is authorized / authorizasation not required
            return await next();
        }
        private bool HasPermission(ClaimsPrincipal user, string permission)
        {
            return user.HasClaim(x => x.Type == ClaimTypes.Spn && x.Value.ToUpper() == permission.ToUpper());
        }
    }
}
