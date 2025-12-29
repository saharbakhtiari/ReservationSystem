using Domain.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.QueueManagers
{
    public interface IQueueManager<TEntity> where TEntity : IEntity
    {
        Task SendAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> ReceiveAsync(CancellationToken cancellationToken);
        int Count();
    }

}
