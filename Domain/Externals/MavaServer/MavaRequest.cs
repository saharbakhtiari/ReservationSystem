using Domain.Common;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.MavaServer
{
    public class MavaRequest : ApiCallerEntity
    {
        [JsonIgnore]
        public IMavaRequestDomainService DomainService { get; set; }
        [JsonIgnore]
        public IMavaRequestRepository Repository { get; set; }
        public MavaRequest()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IMavaRequestDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IMavaRequestRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task<TResponse> SendGetAsync<TResponse>(string path, CancellationToken cancellationToken)
        {
            return await Repository.SendGetAsync<TResponse>(path, cancellationToken);
        }
    }
}
