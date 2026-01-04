using Domain.Common;
using Domain.FileManager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.FileManager
{
    public class FileDriveStorage : IFileStorage
    {
        public Task<long> StoreAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : FileEntity
        {
            byte[] gb = Guid.NewGuid().ToByteArray();
            return Task.FromResult(BitConverter.ToInt64(gb, 0));
        }
    }
}
