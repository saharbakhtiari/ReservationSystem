using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.NotifyServer
{
    public interface INotificationDomainService : IBaseDomainService<Notification>
    {
        Task<bool> CheckEmailExistance(string email, string key, CancellationToken cancellationToken);
    }
}
