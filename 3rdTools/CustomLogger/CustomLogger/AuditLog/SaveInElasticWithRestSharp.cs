using Elasticsearch.Net;
using Extensions;
using log4stash.ElasticClient;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomLoggers.AuditLog;

public class SaveInElasticWithRestSharp: ISaveInElastic, IDisposable
{
    private readonly ICustomLogger<SaveInElasticWithRestSharp> _logger;
    private readonly IElasticSearchConfiguration _elasticSearchConfiguration;
    private readonly IResponseValidator _responseValidator;
    private readonly RestClient _restClient;

    public SaveInElasticWithRestSharp(ICustomLogger<SaveInElasticWithRestSharp> logger,
        IElasticSearchConfiguration elasticSearchConfiguration, 
        IResponseValidator responseValidator)
    {
        _logger = logger;
        _elasticSearchConfiguration = elasticSearchConfiguration;

        if (_elasticSearchConfiguration.SSL && _elasticSearchConfiguration.AllowSelfSignedServerCert)
        {
            ServicePointManager.ServerCertificateValidationCallback += AcceptSelfSignedServerCertCallback;
        }
        _responseValidator = responseValidator;

        _restClient = Create(
            GetServerUrl(),
            _elasticSearchConfiguration.Timeout,
            new BasicAuthenticationMethod() { Username = _elasticSearchConfiguration.AuthUserName, Password = _elasticSearchConfiguration.AuthPassWord }
            );
    }

    public async Task SaveAsync(List<IAuditInfo> auditInfos)
    {
        try
        {
            var request = PrepareRequest(auditInfos);
            await SafeSendRequestAsync(request);
        }
        catch (Exception ex)
        {
            _ = _logger.LogError(ex, ex.Message);
        }
    }
    private RestRequest PrepareRequest(IEnumerable<IAuditInfo> bulk)
    {
        var requestString = PrepareBulk(bulk);
        var restRequest = new RestRequest("_bulk", Method.Post);
        restRequest.AddParameter("application/json", requestString, ParameterType.RequestBody);

        return restRequest;
    }
    private string PrepareBulk(IEnumerable<IAuditInfo> bulk)
    {
        var sb = new StringBuilder();
        foreach (var operation in bulk)
        {
            AddOperationMetadata(sb);
            AddOperationDocument(operation, sb);
        }
        return sb.ToString();
    }
    private void AddOperationMetadata(StringBuilder sb)
    {
        var indexParams = new Dictionary<string, string>()
            {
                { "_index", GetIndexName },
            };
        if (!string.IsNullOrEmpty(_elasticSearchConfiguration.IndexType))
        {
            indexParams["_type"] = _elasticSearchConfiguration.IndexType;
        }
        var paramStrings = indexParams.Where(kv => kv.Value != null)
            .Select(kv => string.Format(@"""{0}"" : ""{1}""", kv.Key, kv.Value));
        var documentMetadata = string.Join(",", paramStrings.ToArray());
        sb.AppendFormat(@"{{ ""index"" : {{ {0} }} }}", documentMetadata);
        sb.Append("\n");
    }
    private static void AddOperationDocument(IAuditInfo operation, StringBuilder sb)
    {
        var jsonSerializeOption = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var json = JsonSerializer.Serialize(operation, jsonSerializeOption);
        sb.Append(json);
        sb.Append("\n");
    }

    private async Task SafeSendRequestAsync(RestRequest request)
    {
        RestResponse response;
        try
        {
            response = await _restClient.ExecuteAsync(request);
        }
        catch (Exception ex)
        {
            _ = _logger.LogError(ex, "Got error while execute request with rest to ElasticSearch");
            return;
        }

        try
        {
            _responseValidator.ValidateResponse(response);
        }
        catch (Exception ex)
        {
            _ = _logger.LogError(ex, "Got error while reading response from ElasticSearch");
        }
    }
    private static Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> ValidateAuthorityIsRoot(X509Certificate certificate)
    {
        return CertificateValidations.AuthorityIsRoot(certificate);
    }
    protected string GetServerUrl()
    {
        var url = string.Format("{0}://{1}:{2}{3}/", _elasticSearchConfiguration.SSL ? "https" : "http", _elasticSearchConfiguration.Address, _elasticSearchConfiguration.Port, _elasticSearchConfiguration.Path.IsNullOrEmpty() ? "" : _elasticSearchConfiguration.Path);
        return url;
    }
    private RestClient Create(string baseUrl, int timeout, IAuthenticator authenticator)
    {

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.DefaultConnectionLimit = 9999;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //var certFile = Path.Combine(/*"D:\\", "cmr-el-cert.p12"*/);
        X509Certificate2 certificate = new X509Certificate2(_elasticSearchConfiguration.CertPath, _elasticSearchConfiguration.CertPass);
        var options = new RestClientOptions() { MaxTimeout = timeout, BaseUrl = new Uri(GetServerUrl()), ClientCertificates = new X509CertificateCollection() { certificate }, Proxy = new WebProxy() };

       // var options = new RestClientOptions() { MaxTimeout = timeout, BaseUrl = new Uri("https://172.18.44.38:9200") };
        options.Authenticator = new HttpBasicAuthenticator(_elasticSearchConfiguration.AuthUserName, _elasticSearchConfiguration.AuthPassWord);
        return new RestClient(options);
    }

    private bool AcceptSelfSignedServerCertCallback(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
    {
        var certificate2 = certificate as X509Certificate2;
        if (certificate2 == null)
            return false;

        string subjectCn = certificate2.GetNameInfo(X509NameType.DnsName, false);
        string issuerCn = certificate2.GetNameInfo(X509NameType.DnsName, true);
        var serverAddresse = _elasticSearchConfiguration.Address;
        if (sslPolicyErrors == SslPolicyErrors.None
            || (serverAddresse.Equals(subjectCn) && subjectCn != null && subjectCn.Equals(issuerCn)))
        {
            return true;
        }

        return false;
    }
    
    private bool _isDisposed = false;
    public void Dispose()
    {
        if (_isDisposed.Not())
        {
            _isDisposed = true;
            Dispose(true);
        }
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ServicePointManager.ServerCertificateValidationCallback -= AcceptSelfSignedServerCertCallback;
        }
    }
    private string GetIndexName => $"{_elasticSearchConfiguration.AuditingLogIndexName}-{DateTime.Now:yyyy-MM}";
}

public class BasicAuthenticationMethod : IAuthenticator
{
    public string Password { get; set; }

    public string Username { get; set; }


    public ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        var authInfo = string.Format("{0}:{1}", Username, Password);
        var encodedAuthInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes(authInfo));
        var authorizationHeaderValue = string.Format("{0} {1}", "Basic", encodedAuthInfo);
        request.AddHeader("Authorization", authorizationHeaderValue);

        return ValueTask.CompletedTask;
    }
}