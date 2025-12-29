using Application.Common.Security;
using Extensions;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IAuthService _authService;

        public LoggingBehaviour(ILogger<TRequest> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var user = await _authService.GetCurrentUserAsync();

            var requestName = typeof(TRequest).Name;
            Guid? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value.To<Guid>();
            string userName = string.Empty;
            string requestSerialized = string.Empty;


            userName = user.FindFirst(ClaimTypes.Name)?.Value;

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
                _logger.LogError(ex, "LoggingBehaviour serializing Error. Something unpredicted has happend when attempting to serialize request content into Json.");
            }
            finally
            {
                _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}",
                    requestName, userId, userName, requestSerialized);
            }

        }
    }
}
