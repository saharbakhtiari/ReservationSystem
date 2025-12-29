using Application.Common.Security;
using Domain.Permissions;
using Domain.Security;
using MediatR;

namespace Application.Books.Commands.DeleteBook;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_NewsManager)]
public class DeleteBookCommand : IRequest
{
    public long Id { get; set; }
}
