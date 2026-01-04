using Application.Cartable.Queries.GetMyCartables;
using Domain.Common;
using Domain.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Queries.GetMyCartables
{
    public class GetMyCartablesQueryHandler : IRequestHandler<GetMyCartablesQuery, PagedList<GetMyCartablesDto>>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetMyCartablesQueryHandler(ICurrentUserService currentUserService)
        {
            this._currentUserService = currentUserService;
        }

        public Task<PagedList<GetMyCartablesDto>> Handle(GetMyCartablesQuery request, CancellationToken cancellationToken)
        {
            return new Domain.Cartables.Cartable().Repository.GetMyCartablesAsync<GetMyCartablesDto>(_currentUserService.UserId,request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
