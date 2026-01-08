using Application.Spaces.Queries.GetFilteredSpaces;
using Domain.Common;
using Domain.Spaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Spaces.Queries.GetFilteredSpace
{
    public class GetFilteredSpacesQueryHandler : IRequestHandler<GetFilteredSpacesQuery, PagedList<FilteredSpacesDto>>
    {
        public Task<PagedList<FilteredSpacesDto>> Handle(GetFilteredSpacesQuery request, CancellationToken cancellationToken)
        {
            return new Space().Repository.GetFilteredAsync<FilteredSpacesDto>( request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
