using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer
{
    public interface INotificationRepository : IGenericApiCallerRepository<Notification>
    {
        Task Login(CancellationToken cancellationToken);
        Task RefreshToken(CancellationToken cancellationToken);
        Task<TResponse> SendAsync<TResponse>(string path, CancellationToken cancellationToken);
    }
}
