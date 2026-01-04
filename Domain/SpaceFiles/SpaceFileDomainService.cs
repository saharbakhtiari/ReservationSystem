using Domain.Common;
using Domain.FileManager;
using Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SpaceFiles
{
    public class SpaceFileDomainService : ISpaceFileDomainService
    {
        private readonly IFileStorageConfiguration _fileStorageConfiguration;
        private readonly IFileStorage _fileStorage;

        public SpaceFileDomainService(IFileStorageConfiguration fileStorageConfiguration)
        {
            _fileStorageConfiguration = fileStorageConfiguration;
            _fileStorage = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IFileStorage>(_fileStorageConfiguration.FileStorage);
        }

        public SpaceFile OwnerEntity { get; set; }

        public Task<long> StoreFile(CancellationToken cancellationToken)
        {
            return _fileStorage.StoreAsync(OwnerEntity, cancellationToken);
        }
    }
}
