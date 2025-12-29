using DocumentFormat.OpenXml.Packaging;
using Domain.FileManager;
using System.IO;

namespace Infrastructure.FileManager
{
    public class WordFileFilter : IFileFilter
    {
        public byte[] Filter(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
            {
                doc.PackageProperties.Subject = null;
                doc.PackageProperties.Title = null;
                doc.PackageProperties.Description = null;
                doc.PackageProperties.Keywords = null;
                doc.PackageProperties.Creator = null;
                doc.PackageProperties.LastModifiedBy = null;
                doc.Save();
                return memoryStream.ToArray();
            }
        }
    }
}
