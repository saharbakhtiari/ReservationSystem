using Elasticsearch.Net;
using Extensions;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CustomLoggers.AuditLog;

public class SaveInElasticWithNest : ISaveInElastic
{
    private readonly ICustomLogger<SaveInElasticWithNest> _logger;
    private readonly IElasticSearchConfiguration _elasticSearchConfiguration;
    private readonly Lazy<IElasticClient> _client;

    private IElasticClient ElasticClient => _client.Value;

    public SaveInElasticWithNest(ICustomLogger<SaveInElasticWithNest> logger,
        IElasticSearchConfiguration elasticSearchConfiguration)
    {
        _logger = logger;
        _elasticSearchConfiguration = elasticSearchConfiguration;
        
        _client = new Lazy<IElasticClient>(CreatElasticClient);
    }
    private static Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> ValidateAuthorityIsRoot( X509Certificate certificate)
    {
        return CertificateValidations.AuthorityIsRoot(certificate);
    }
    private IElasticClient CreatElasticClient()
    {
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.DefaultConnectionLimit = 9999;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
       // var certFile = Path.Combine(_elasticSearchConfiguration.CertPath/*"D:\\", "cmr-el-cert.p12"*/);
        X509Certificate2 certificate = new X509Certificate2(_elasticSearchConfiguration.CertPath, _elasticSearchConfiguration.CertPass);

        var str = GetServerUrl();
        var strs = str.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        var nodes = strs.Select(s => new Uri(s)).ToList();
        var connectionPool = new StaticConnectionPool(nodes);
        var res = ValidateAuthorityIsRoot(certificate);
        var settings = new ConnectionSettings(connectionPool)
            .BasicAuthentication(_elasticSearchConfiguration.AuthUserName, _elasticSearchConfiguration.AuthPassWord)
            .ServerCertificateValidationCallback(res);
        if (_elasticSearchConfiguration.AllowSelfSignedServerCert)
        {
            settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
        }

        return new ElasticClient(settings);
    }
    public async Task SaveAsync(List<IAuditInfo> auditInfos)
    {
        try
        {
            var indexManyResponse = await ElasticClient.IndexManyAsync(auditInfos, GetIndexName);

            if (indexManyResponse.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in indexManyResponse.ItemsWithErrors)
                {
                    // if there are errors, they can be enumerated and inspected
                    _ = _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }

        }
        catch (Exception ex)
        {
            _ = _logger.LogError(ex, ex.Message);
        }
    }
    
    protected string GetServerUrl()
    {
        var url = string.Format("{0}://{1}:{2}{3}/", _elasticSearchConfiguration.SSL ? "https" : "http", _elasticSearchConfiguration.Address, _elasticSearchConfiguration.Port, _elasticSearchConfiguration.Path.IsNullOrEmpty() ? "" : _elasticSearchConfiguration.Path);
        return url;
    }

    private string GetIndexName => $"{_elasticSearchConfiguration.AuditingLogIndexName}-{DateTime.Now:yyyy-MM}";
}