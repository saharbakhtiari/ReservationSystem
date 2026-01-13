using Application.CancellationPolicys.Commands.CreateCancellationPolicy;
using Domain.Contract.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.CancellationPolicys.Commands.UpdateCancellationPolicy
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_CancellationPolicyManager)]
    public class UpdateCancellationPolicyCommand : IRequest
    {
        public long Id { get; set; }
        public long TariffId { get; set; }
        public int FreeCancelUntilHours { get; set; }
        public int PenaltyPercentAfter { get; set; }
        public int NoShowPenalty { get; set; }
    }

}
