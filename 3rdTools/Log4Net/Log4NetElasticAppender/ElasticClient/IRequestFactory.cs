using System.Collections.Generic;
using RestSharp;

namespace log4stash.ElasticClient
{
    public interface IRequestFactory
    {
        RestRequest PrepareRequest(IEnumerable<InnerBulkOperation> bulk);
        RestRequest CreatePutTemplateRequest(string templateName, string rawBody);
    }
}