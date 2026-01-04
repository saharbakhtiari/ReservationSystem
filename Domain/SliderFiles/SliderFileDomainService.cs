using Domain.Common;
using Domain.FileManager;
using Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SliderFiles
{
    public class SliderFileDomainService : ISliderFileDomainService
    {
        private readonly IFileStorageConfiguration _fileStorageConfiguration;
        private readonly IFileStorage _fileStorage;

        public SliderFileDomainService(IFileStorageConfiguration fileStorageConfiguration)
        {
            _fileStorageConfiguration = fileStorageConfiguration;
            _fileStorage = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IFileStorage>(_fileStorageConfiguration.FileStorage);
        }

        public SliderFile OwnerEntity { get; set; }

        public Task<long> StoreFile(CancellationToken cancellationToken)
        {
            return _fileStorage.StoreAsync(OwnerEntity, cancellationToken);
        }
    }
}
