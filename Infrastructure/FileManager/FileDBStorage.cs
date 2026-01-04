
using Domain.Common;
using Domain.FileManager;
using Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.FileManager
{
    public class FileDBStorage : IFileStorage
    {

        public async Task<long> StoreAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : FileEntity
        {
            var type = entity.Name.Split('.')?.Last();
            var fileFilter = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IFileFilter>(type.ToLower());
            entity.DataFiles = fileFilter.Filter(entity.DataFiles);
            await entity.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
