using Domain.Common;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer
{
    public class Notification : ApiCallerEntity
    {
        [JsonIgnore]
        public INotificationDomainService DomainService { get; set; }
        [JsonIgnore]
        public INotificationRepository Repository { get; set; }
        public Notification()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<INotificationDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<INotificationRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task<TResponse> SendGetAsync<TResponse>(string path, CancellationToken cancellationToken)
        {
            return await Repository.SendAsync<TResponse>(path, cancellationToken);
        }
    }
}
