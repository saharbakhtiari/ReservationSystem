using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.CMRServer
{
    public interface ICMRRequestRepository : IGenericApiCallerRepository<CMRRequest>
    {
        Task Login(CancellationToken cancellationToken);
        Task RefreshToken(CancellationToken cancellationToken);
        Task<TResponse> SendGetAsync<TResponse>(string path, CancellationToken cancellationToken);
    }
}
