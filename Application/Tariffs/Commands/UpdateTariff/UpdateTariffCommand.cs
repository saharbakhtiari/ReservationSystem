using Application.Tariffs.Commands.CreateTariff;
using Domain.Contract.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.Tariffs.Commands.UpdateTariff
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_TariffManager)]
    public class UpdateTariffCommand : IRequest
    {
        public long Id { get; set; }
        public long SpaceId { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public TariffUnit Unit { get; set; }
        public string Rules { get; set; }
    }

}
