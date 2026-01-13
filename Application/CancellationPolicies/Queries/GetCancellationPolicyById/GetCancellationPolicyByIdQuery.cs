using MediatR;

namespace Application.CancellationPolicys.Queries.GetCancellationPolicy
{
    public class GetCancellationPolicyByIdQuery : IRequest<GetCancellationPolicyByIdDto>
    {
        public long Id { get; set; }
    }
}
