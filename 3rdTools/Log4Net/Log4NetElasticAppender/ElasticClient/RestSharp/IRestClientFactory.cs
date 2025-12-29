using RestSharp;
using RestSharp.Authenticators;

namespace log4stash.ElasticClient.RestSharp
{
    public interface IRestClientFactory
    {
        RestClient Create(string baseUrl, int timeout, IAuthenticator authenticator, string CertPath, string CertPass);
    }
}