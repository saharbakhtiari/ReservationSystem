using Microsoft.AspNetCore.Http;
using System.IO;

namespace Domain.Footers
{
    public class FooterDomainService : IFooterDomainService
    {
        public Footer OwnerEntity { get; set; }

    }
}
