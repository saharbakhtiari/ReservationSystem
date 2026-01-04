using Domain.Common;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.CMRServer
{
    public class CMRRequest : ApiCallerEntity
    {
        [JsonIgnore]
        public ICMRRequestDomainService DomainService { get; set; }
        [JsonIgnore]
        public ICMRRequestRepository Repository { get; set; }
        public CMRRequest()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ICMRRequestDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ICMRRequestRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task<TResponse> SendGetAsync<TResponse>(string path, CancellationToken cancellationToken)
        {

            return await Repository.SendGetAsync<TResponse>(path, cancellationToken);
        }
    }
}
