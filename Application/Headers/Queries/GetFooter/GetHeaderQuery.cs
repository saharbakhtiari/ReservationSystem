using MediatR;

namespace Application.Headers.Queries.GetHeader
{
    public class GetHeaderQuery : IRequest<HeaderDto>
    {
        public long Id { get; set; }
    }
}
