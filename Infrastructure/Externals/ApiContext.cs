using System.Net.Http;

namespace Infrastructure.Externals
{
    public class ApiContext : IApiContext
    {
        public HttpClient Client { get; }
        public string Token { get; set; }

        public ApiContext()
        {
            Client = new HttpClient();
        }
    }
}
