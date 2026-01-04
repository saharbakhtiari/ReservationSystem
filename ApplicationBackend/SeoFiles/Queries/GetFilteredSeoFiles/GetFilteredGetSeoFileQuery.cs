using Application.SeoFiles.Queries.GetFilteredSeoFiles;
using Domain.Common;
using Domain.SeoFiles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.SeoFiles.Queries.GetFilteredSeoFiles
{
    public class GetFilteredSeoFilesQueryHandler : IRequestHandler<GetFilteredSeoFilesQuery, PagedList<FilteredSeoFileDto>>
    {
        public Task<PagedList<FilteredSeoFileDto>> Handle(GetFilteredSeoFilesQuery request, CancellationToken cancellationToken)
        {
            return new SeoFile().Repository.GetFilteredAsync<FilteredSeoFileDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
