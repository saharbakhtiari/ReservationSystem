using Domain.Common;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.FileManager
{
    public interface IFileStorage
    {
        void Remove(string path);
        Task<string> StoreAsync(IFormFile entity, CancellationToken cancellationToken, string path = null);
    }
}
