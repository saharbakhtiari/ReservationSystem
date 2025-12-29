using Application.Common.Security;
using CustomLoggers.AuditLog;
using Domain.Common;
using Domain.Common.Interfaces;
using Exceptions;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Common.Behaviours
{
    public class AuditBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;

        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditInfo _auditInfo;
        private readonly IAmbientAuditLogActionInfo _ambientAuditLogActionInfo;

        public AuditBehaviour(
            )
        {
            _timer = new Stopwatch();

            _currentUserService = ServiceLocator.ServiceProvider.GetService<ICurrentUserService>();
            _auditInfo = ServiceLocator.ServiceProvider.GetService<IAuditInfo>();
            _ambientAuditLogActionInfo = ServiceLocator.ServiceProvider.GetService<IAmbientAuditLogActionInfo>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            AuditLogActionInfo auditLogActionInfo = new();
            string requestSerialized = string.Empty;

            try
            {
                JsonSerializerSettings removeSensitiveInformationSettings = new JsonSerializerSettings
                {
                    ContractResolver = new IgnoreSensitivePropertiesResolver()
                };
                requestSerialized = JsonConvert.SerializeObject(request, removeSensitiveInformationSettings);
            }
            catch
            {
            }

            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            var userName = string.Empty;

            userName = _currentUserService.UserName;


            auditLogActionInfo.MethodName = requestName;
            auditLogActionInfo.Parameters = requestSerialized;

            if (userId is not null && userId != Guid.Empty)
            {
                auditLogActionInfo.ExtraProperties.Add("userId", userId);
                auditLogActionInfo.ExtraProperties.Add("userName", userName);
            }
            auditLogActionInfo.ExecutionTime = DateTime.Now;

            _ambientAuditLogActionInfo.SetAuditLogActionInfo(auditLogActionInfo);

            _timer.Start();
            TResponse response;
            try
            {
                response = await next();
                
                return response;
            }
            catch (UserFriendlyException ex)
            {
                auditLogActionInfo.ExtraProperties.Add("UFEMessage", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                auditLogActionInfo.Exceptions = ex;
                throw;
            }
            finally
            {
                _timer.Stop();

                var elapsedMilliseconds = _timer.ElapsedMilliseconds;
                auditLogActionInfo.ExecutionDuration = (int)elapsedMilliseconds;

                _auditInfo.Actions.Add(auditLogActionInfo);
            }

        }
    }
}
