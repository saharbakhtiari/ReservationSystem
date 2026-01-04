using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace log4stash.ElasticClient
{
    public class RequestFactory : IRequestFactory
    {
        public RestRequest PrepareRequest(IEnumerable<InnerBulkOperation> bulk)
        {
            var requestString = PrepareBulk(bulk);
            var restRequest = new RestRequest("_bulk", Method.Post);
            restRequest.AddParameter("application/json", requestString, ParameterType.RequestBody);

            return restRequest;
        }

        public RestRequest CreatePutTemplateRequest(string templateName, string rawBody)
        {
            var url = string.Concat("_template/", templateName);
            var restRequest = new RestRequest(url, Method.Put) { RequestFormat = DataFormat.Json };
            restRequest.AddParameter("application/json", rawBody, ParameterType.RequestBody);
            return restRequest;
        }

        private static string PrepareBulk(IEnumerable<InnerBulkOperation> bulk)
        {
            var sb = new StringBuilder();
            foreach (var operation in bulk)
            {
                AddOperationMetadata(operation, sb);
                AddOperationDocument(operation, sb);
            }
            return sb.ToString();
        }

        private static void AddOperationMetadata(InnerBulkOperation operation, StringBuilder sb)
        {
            var indexParams = new Dictionary<string, string>(operation.IndexOperationParams)
            {
                { "_index", operation.IndexName },
            };
            //if (!string.IsNullOrEmpty(operation.IndexType))
            //{
            //    indexParams["_type"] = operation.IndexType;
            //}
            var paramStrings = indexParams.Where(kv => kv.Value != null)
                .Select(kv => string.Format(@"""{0}"" : ""{1}""", kv.Key, kv.Value));
            var documentMetadata = string.Join(",", paramStrings.ToArray());
            sb.AppendFormat(@"{{ ""index"" : {{ {0} }} }}", documentMetadata);
            sb.Append("\n");
        }

        private static void AddOperationDocument(InnerBulkOperation operation, StringBuilder sb)
        {
            var json = JsonConvert.SerializeObject(operation.Document);
            sb.Append(json);
            sb.Append("\n");
        }


    }
}
