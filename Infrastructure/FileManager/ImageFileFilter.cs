using Domain.FileManager;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;



//using ImageMagick;
using System.IO;

namespace Infrastructure.FileManager
{
    public class ImageFileFilter : IFileFilter
    {
        public byte[] Filter(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using var image = Image.Load(memoryStream);

                image.Metadata.ExifProfile = null;
                image.Metadata.IptcProfile = null;
                image.Metadata.XmpProfile = null;
                image.Metadata.IccProfile = null;

                using var stream = new MemoryStream();


                image.Save(stream, JpegFormat.Instance);

                //await image.WriteAsync("images/flickr-without-metadata.jpg");
                return stream.ToArray();
            }
        }
    }
}
