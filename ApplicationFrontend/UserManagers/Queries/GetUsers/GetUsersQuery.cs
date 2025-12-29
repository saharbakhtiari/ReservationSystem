using Application.UserManagers.Queries.GetUsers;
using Application_Frontend.Common;
using Domain.Common;
using Domain.Users;
using Extensions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.UserManagers.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedList<UserDto>>
    {
        private readonly IClientSideRequestHandler _clientRequestHandler;
        public GetUsersQueryHandler(IClientSideRequestHandler clientRequestHandler)
        {
            _clientRequestHandler = clientRequestHandler;
        }

        public async Task<PagedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var url = string.Format(ApiAddress.GetUsers, request.PageNumber, request.PageSize, request.Sort.IsNullOrWhiteSpace() ? " " : request.Sort, request.Filter);
            var response = await _clientRequestHandler.GetPagingListAsync(url);
            var items = JsonSerializer.Deserialize<List<UserDto>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var MetaData = JsonSerializer.Deserialize<PagedListMetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var result = new PagedList<UserDto>(items, MetaData.TotalCount, MetaData.CurrentPage, MetaData.PageSize);
            return result;
        }
    }
}
