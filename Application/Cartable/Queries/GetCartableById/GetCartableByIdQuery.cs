using MediatR;

namespace Application.Cartable.Queries.GetCartableById
{
    public class GetCartableByIdQuery : IRequest<CartableDto>
    {
        public long Id { get; set; }
    }
}
