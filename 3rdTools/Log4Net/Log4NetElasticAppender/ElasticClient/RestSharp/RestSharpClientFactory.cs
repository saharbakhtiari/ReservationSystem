using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace log4stash.ElasticClient.RestSharp
{
    public class RestSharpClientFactory : IRestClientFactory
    {
        public RestClient Create(string baseUrl, int timeout, IAuthenticator authenticator, string CertPath, string CertPass)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            X509Certificate2 certificate = new X509Certificate2(CertPath, CertPass);

            var options = new RestClientOptions()
            {
                MaxTimeout = timeout,
                BaseUrl = new Uri(baseUrl),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                Authenticator = authenticator,
                ClientCertificates = new X509CertificateCollection() { certificate },
                Proxy = new WebProxy()
            };
            return new RestClient(options);
        }
    }
}
