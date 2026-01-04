using MediatR;

namespace Application.Footers.Queries.GetFooter
{
    public class GetFooterQuery : IRequest<FooterDto>
    {
        public long Id { get; set; }
    }
}
