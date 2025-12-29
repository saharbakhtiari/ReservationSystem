using Application.Common.Security;
using Domain.Common;
using Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagers.Queries.GetSuggestUsers
{
    [Authorize]
    public class GetSuggestUsersQuery : IRequest<List<UserInputDto>>
    {
        public string Filter { get; set; }
    }
}
