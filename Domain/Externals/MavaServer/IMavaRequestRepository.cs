using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.MavaServer
{
    public interface IMavaRequestRepository : IGenericApiCallerRepository<MavaRequest>
    {
        Task Login(CancellationToken cancellationToken);
        Task RefreshToken(CancellationToken cancellationToken);
        Task<TResponse> SendGetAsync<TResponse>(string path, CancellationToken cancellationToken);
    }
}
