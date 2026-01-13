using MediatR;

namespace Application.Tariffs.Commands.DeleteTariff;

//[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_TariffManager)]
public class DeleteTariffCommand : IRequest
{
    public long Id { get; set; }
}
