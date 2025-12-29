using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Common.Interfaces
{
    public interface INotifyUsers
    {
        Task NotifyUserAsync(Guid userId, NotifyType notifyType, object extraData);
        Task NotifyUserListAsync(List<Guid> userIds, NotifyType notifyType, object extraData);

    }
}
