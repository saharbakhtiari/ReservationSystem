using Application.RoleManagers.Queries.GetRoles;
using Application_Frontend.Common;
using Extensions;
using Domain.Common;
using Domain.Roles;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.RoleManagers.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedList<RoleDto>>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public GetRolesQueryHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<PagedList<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var url = string.Format(ApiAddress.GetRoles, request.PageNumber, request.PageSize, request.Sort.IsNullOrWhiteSpace() ? " " : request.Sort, request.Filter);
            var response = await _clientRequestHandler.GetPagingListAsync(url);
            var items = JsonSerializer.Deserialize<List<RoleDto>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var MetaData = JsonSerializer.Deserialize<PagedListMetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var result = new PagedList<RoleDto>(items, MetaData.TotalCount, MetaData.CurrentPage, MetaData.PageSize);
            return result;
        }
    }
}
