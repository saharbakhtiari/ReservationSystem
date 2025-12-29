//using Domain.Common;
//using Domain.FileManager;
//using Extensions;
//using Microsoft.AspNetCore.Http;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Infrastructure.FileManager
//{
//    public class FileDBStorage : IFileStorage
//    {
//        public async Task<string> StoreAsync(IFormFile entity, CancellationToken cancellationToken, string path = null)
//        {
//            //var type = entity.Name.Split('.')?.Last();
//            //var fileFilter = ServiceLocator.ServiceProvider.GetServiceProvider().GetServiceWithName<IFileFilter>(type);
//            //entity.DataFiles = fileFilter.Filter(entity.DataFiles);
//            //await entity.SaveAsync(cancellationToken);
//            //return entity.Id;
//            return "";
//        }
//    }
//}
