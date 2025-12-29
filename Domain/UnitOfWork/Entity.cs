using System.Threading;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
{

    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public abstract Task SaveAsync(CancellationToken cancellationToken);
    }
}
