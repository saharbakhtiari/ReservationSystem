using Application.Common.Security;
using CustomLoggers;
using Domain.Common;
using Domain.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ICustomLogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserManager _accountService;
        private readonly IGetClientInfo _getClientInfo;

        public LoggingBehaviour(ICustomLogger<TRequest> logger/*, ICurrentUserService currentUserService, IUserManager identityService*/)
        {
            _logger = logger;
            _currentUserService = ServiceLocator.ServiceProvider.GetService<ICurrentUserService>();//currentUserService;
            _accountService = ServiceLocator.ServiceProvider.GetService<IUserManager>(); //accountService;
            _getClientInfo = ServiceLocator.ServiceProvider.GetService<IGetClientInfo>(); //accountService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            Guid? userId = _currentUserService.UserId;
            string userName = string.Empty;
            string requestSerialized = string.Empty;
            string clientIp = _getClientInfo.ClientIp;

            userName = _currentUserService.UserName;

            if (string.IsNullOrWhiteSpace(userName))
            {
                // try to get user name from a different source (likely slower)
                if (userId != null && userId.Value != Guid.Empty)
                {
                    // This will likely hit the database
                    userName = await _accountService.GetUserNameAsync(userId.GetValueOrDefault());
                }
            }

            try
            {
                JsonSerializerSettings removeSensitiveInformationSettings = new JsonSerializerSettings
                {
                    ContractResolver = new IgnoreSensitivePropertiesResolver()
                };
                requestSerialized = JsonConvert.SerializeObject(request, removeSensitiveInformationSettings);
            }
            catch (JsonSerializationException jse)
            {
                // Exception caught from attempting to serialize request content to Json.
                // This could happen when trying to serialize Streams (Uploading images for example) - and we don't need to know about this remotely.
            }
            catch (Exception ex)
            {
                _ = _logger.LogError(ex, "LoggingBehaviour serializing Error. Something unpredicted has happend when attempting to serialize request content into Json.");
            }
            finally
            {
                _ = _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Ip} {@Request}",
                    requestName, userId, userName, clientIp, requestSerialized);
            }
        }
    }
}
