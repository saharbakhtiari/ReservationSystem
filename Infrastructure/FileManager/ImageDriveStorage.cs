using Domain.FileManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.FileManager
{
    public class ImageDriveStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;

        public ImageDriveStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> StoreAsync(IFormFile file, CancellationToken cancellationToken, string path = null)
        {
            var ext = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + ext;
            var folderPath = Path.Combine(_env.WebRootPath, path);
            var fullPath = Path.Combine(folderPath, fileName);
            if (ext.ToLowerInvariant() == ".svg")
            {
                await StoreDirectlyAsync(ext, fullPath, file, cancellationToken);
            }
            else
            {
                await StoreCompressedAsync(folderPath, fullPath, file, cancellationToken);
            }
            return $"/{path}/{fileName}";
        }

        public void Remove(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }
            var fullPath = Path.Combine(_env.WebRootPath, path.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        private async Task StoreDirectlyAsync(string ext, string fullPath, IFormFile file, CancellationToken cancellationToken)
        {

            await using var dirStream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(dirStream, cancellationToken);

        }

        private async Task StoreCompressedAsync(string folderPath, string fullPath, IFormFile file, CancellationToken cancellationToken)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using var stream = file.OpenReadStream();
            using var image = await Image.LoadAsync(stream, cancellationToken);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(800, 0),
                Mode = ResizeMode.Max
            }));
            var encoder = new JpegEncoder { Quality = 75 };
            await using var outStream = new FileStream(fullPath, FileMode.Create);
            await image.SaveAsJpegAsync(outStream, encoder, cancellationToken);
        }
    }
}
