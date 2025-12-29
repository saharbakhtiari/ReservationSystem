using System.Net.Http;
using System.Threading.Tasks;

namespace Application_Frontend.Common
{
    public interface IClientSideRequestHandler
    {
        Task<TResponse> GetAsync<TResponse>(string url);
        Task<HttpResponseMessage> GetPagingListAsync(string url);
        Task<HttpResponseMessage> GetPagingListAsync<TRequst>(TRequst queryModel, string url);
        Task<TResponse> PostAsync<TResponse, TRequst>(TRequst registerModel, string url);
    }
}
