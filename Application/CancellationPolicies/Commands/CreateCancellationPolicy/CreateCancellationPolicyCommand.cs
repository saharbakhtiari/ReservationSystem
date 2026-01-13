using Domain.Contract.Enums;
using Domain.Spaces;
using Domain.Tariffs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.CancellationPolicys.Commands.CreateCancellationPolicy
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_CancellationPolicyManager)]
    public class CreateCancellationPolicyCommand : IRequest<long>
    {
        public long TariffId { get; set; } 
        public int FreeCancelUntilHours { get; set; }
        public int PenaltyPercentAfter { get; set; }
        public int NoShowPenalty { get; set; }
    }
}
