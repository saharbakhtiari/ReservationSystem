using System.Net.Http;

namespace Infrastructure.Externals
{
    public interface IApiContext
    {
        HttpClient Client { get; }
        string Token { get; set; }
    }
}
