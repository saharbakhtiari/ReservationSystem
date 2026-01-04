using Application.Common.Security;
using CustomLoggers;
using Extensions;
using Domain.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;

namespace Application_Backend.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICustomLogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserManager _accountService;
        private readonly IRequestOrginService _requestOrginService;

        public AuthorizationBehaviour(
            ICustomLogger<TRequest> logger
            //ICurrentUserService currentUserService,
            //IUserManager accountService
            )
        {
            _logger = logger;
            _currentUserService = ServiceLocator.ServiceProvider.GetService<ICurrentUserService>();//currentUserService;
            _accountService = ServiceLocator.ServiceProvider.GetService<IUserManager>(); //accountService;
            _requestOrginService = ServiceLocator.ServiceProvider.GetService<IRequestOrginService>(); //accountService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType()
                .GetCustomAttributes<AuthorizeAttribute>();

            if (_requestOrginService.IsFromClient && authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId is null)
                {
                    throw new UnauthorizedAccessException();
                }
                if ((await _accountService.IsValidToken(_currentUserService.UserId.Value, _currentUserService.UserKey.Value)).Not())
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
                                var isInRole = await _accountService.UserIsInRoleAsync(_currentUserService.UserId.Value, role.Trim());
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
                                var hasPermission = await _accountService.CheckPermissionAsync(_currentUserService.UserId.Value, permission.Trim());
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
    }
}
