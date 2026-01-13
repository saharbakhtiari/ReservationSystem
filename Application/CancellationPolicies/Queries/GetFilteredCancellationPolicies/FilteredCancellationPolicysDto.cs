using Domain.Contract.Enums;

namespace Application.CancellationPolicys.Queries.GetFilteredCancellationPolicys
{
    public class FilteredCancellationPolicysDto
    {
        public long Id { get; set; }
        public int FreeCancelUntilHours { get; set; }
        public int PenaltyPercentAfter { get; set; }
        public int NoShowPenalty { get; set; }
    }
}
