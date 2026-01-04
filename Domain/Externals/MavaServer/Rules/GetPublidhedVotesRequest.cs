using Domain.Contract.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Externals.MavaServer.Rules
{
    public class GetPublidhedVotesRequest : MavaRequest
    {
        public bool IsLegal { get; set; }
        public Task<List<PublidhedVotesResponse>> SendAsync(CancellationToken cancellationToken)
        {
            return base.SendGetAsync<List<PublidhedVotesResponse>>($"{MavaServerApis.GetPublishedVotes}?IsLegal={this.IsLegal}", cancellationToken);
        }
    }
    public class PublidhedVotesResponse
    {

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("personName")]
        public string PersonName { get; set; }

        [JsonPropertyName("personFamily")]
        public string PersonFamily { get; set; }

        [JsonPropertyName("personNationalCode")]
        public string PersonNationalCode { get; set; }

        [JsonPropertyName("colorCode")]
        public int ColorCode { get; set; }

        [JsonPropertyName("accusationSubjects")]
        public string AccusationSubjects { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("crimeDate")]
        public string CrimeDate { get; set; }

        [JsonPropertyName("isLegal")]
        public bool IsLegal { get; set; }
    }
}
