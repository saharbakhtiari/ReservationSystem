using Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebAppBlazor.Server.Services
{
    public class RequestOrginService : IRequestOrginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RequestOrginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsFromClient => _httpContextAccessor?.HttpContext is null ? false : true;
    }
}
