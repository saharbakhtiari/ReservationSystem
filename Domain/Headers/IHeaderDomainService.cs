using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Domain.Headers
{
    public interface IHeaderDomainService : IBaseDomainService<Header>
    {
        void SetFile(IFormFile file);
    }
}
