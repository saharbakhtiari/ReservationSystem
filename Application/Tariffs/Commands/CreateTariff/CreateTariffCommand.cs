using Domain.Contract.Enums;
using Domain.Spaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Tariffs.Commands.CreateTariff
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_TariffManager)]
    public class CreateTariffCommand : IRequest<long>
    {
        public long SpaceId { get; set; } 
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
        public string Rules { get; set; }
    }
}
