using MediatR;

namespace Application.Spaces.Queries.GetSpace
{
    public class GetSpaceByIdQuery : IRequest<GetSpaceByIdDto>
    {
        public long Id { get; set; }
    }
}
