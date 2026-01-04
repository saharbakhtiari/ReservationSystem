using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.FileManager
{
    public interface IFileStorage
    {
        Task<long> StoreAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : FileEntity;
    }
}
