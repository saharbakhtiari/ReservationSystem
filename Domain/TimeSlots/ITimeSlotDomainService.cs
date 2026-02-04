using Domain.Common;
using Domain.Spaces;
using Domain.Tariffs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.TimeSlots
{
    public interface ITimeSlotDomainService : IBaseDomainService<TimeSlot>
    {
        List<TimeSlot> GenerateDailyTimeSlot(DateTime startDate, DateTime endDate, Space space, Tariff tariff);
        List<TimeSlot> GenerateHourlyTimeSlot(TimeSpan startTime, TimeSpan endTime, DateTime startDate, DateTime? endDate, Space space, Tariff tariff, int intervalHours);
        Task SetSpace(long spaceId, CancellationToken cancellationToken);
    }
}
