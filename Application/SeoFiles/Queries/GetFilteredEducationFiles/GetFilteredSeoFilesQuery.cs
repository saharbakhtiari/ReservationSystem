using Application.SeoFiles.Queries.GetFilteredSeoFiles;
using Domain.Common;
using MediatR;

namespace Application_Backend.SeoFiles.Queries.GetFilteredSeoFiles
{
    public class GetFilteredSeoFilesQuery : IRequest<PagedList<FilteredSeoFileDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
