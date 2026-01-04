using Domain.Common;
using Domain.Permissions;
using Domain.Security;
using Application.Common.Security;

using MediatR;

namespace Application.Cartable.Queries.GetMyCartables
{
    [Authorize]
    public class GetMyCartablesQuery : IRequest<PagedList<GetMyCartablesDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
