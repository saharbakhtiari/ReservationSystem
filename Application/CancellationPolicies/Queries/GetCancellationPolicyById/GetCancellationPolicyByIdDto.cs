using Domain.Contract.Enums;
using Domain.Spaces;

namespace Application.CancellationPolicys.Queries.GetCancellationPolicy
{
    public class GetCancellationPolicyByIdDto
    {
        public long Id { get; set; }
        public GetCancellationPolicyByIdTariffDto Tariff { get; set; }
        public int FreeCancelUntilHours { get; set; }
        public int PenaltyPercentAfter { get; set; }
        public int NoShowPenalty { get; set; }
    }
    public class GetCancellationPolicyByIdTariffDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
        public string Rules { get; set; }
    }

}
