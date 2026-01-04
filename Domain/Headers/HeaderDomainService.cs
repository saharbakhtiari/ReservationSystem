using Microsoft.AspNetCore.Http;
using System.IO;

namespace Domain.Headers
{
    public class HeaderDomainService : IHeaderDomainService
    {
        public Header OwnerEntity { get; set; }

        public void SetFile(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                OwnerEntity.DataFiles = ms.ToArray();
            }
        }
    }
}
