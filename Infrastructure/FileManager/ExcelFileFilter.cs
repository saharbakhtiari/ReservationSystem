using DocumentFormat.OpenXml.Packaging;
using Domain.FileManager;
using System.IO;

namespace Infrastructure.FileManager
{
    public class ExcelFileFilter : IFileFilter
    {
        public byte[] Filter(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(memoryStream, true))
            {
                var properties = doc.PackageProperties;
                doc.PackageProperties.Subject = null;
                doc.PackageProperties.Title = null;
                doc.PackageProperties.Description = null;
                doc.PackageProperties.Keywords = null;
                doc.PackageProperties.Creator = null;
                doc.PackageProperties.LastModifiedBy = null;
                // Set other properties to null as needed
                doc.Save();
                return memoryStream.ToArray();
            }
        }
    }
}
