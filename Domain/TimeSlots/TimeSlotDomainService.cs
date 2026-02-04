using Domain.Common.Interfaces;
using Domain.Contract.Enums;
using Domain.Spaces;
using Domain.Tariffs;
using Domain.UnitOfWork.Uow;
using Exceptions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.TimeSlots
{
    public class TimeSlotDomainService : ITimeSlotDomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStringLocalizer _localizer;
        private readonly ICurrentUserService _currentUserService;


        public TimeSlotDomainService(IUnitOfWorkManager unitOfWorkManager, IStringLocalizer localizer, ICurrentUserService currentUserService)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizer = localizer;
            _currentUserService = currentUserService;
        }

        public TimeSlot OwnerEntity { get; set; }
        public async Task SetSpace(long spaceId, CancellationToken cancellationToken)
        {
            if (spaceId > 0)
            {
                var space = await Space.GetAsync(spaceId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
                OwnerEntity.Space = space;
            }
        }

   
        public List<TimeSlot> GenerateHourlyTimeSlot(TimeSpan startTime,
                                                     TimeSpan endTime,
                                                     DateTime startDate,
                                                     DateTime? endDate,
                                                     Space space,
                                                     Tariff tariff,
                                                     int intervalHours)
        {
            List<TimeSlot> timeSlots = new();
            var currentDate = startDate.Date;
            var end = endDate.HasValue && endDate.Value != DateTime.MinValue ? endDate.Value : startDate;
                while (currentDate <= end)
                {
                    var currentTime = startTime;
                    while (currentTime.Add(TimeSpan.FromHours(intervalHours)) <= endTime)
                    {
                        timeSlots.Add(new TimeSlot()
                        {
                            Type = TimeSlotType.Hourly,
                            StartAt = currentTime,
                            EndAt = currentTime.Add(TimeSpan.FromHours(intervalHours)),
                            SlotDate = currentDate,
                            Space = space,
                            Tariff = tariff
                        });
                        currentTime = currentTime.Add(TimeSpan.FromHours(intervalHours));
                    }
                    currentDate = currentDate.AddDays(1);
                }
            return timeSlots;
        }

        public List<TimeSlot> GenerateDailyTimeSlot(DateTime startDate,
                                                    DateTime endDate,
                                                    Space space,
                                                    Tariff tariff)
        {
            List<TimeSlot> timeSlots = new();
            var current = startDate.Date;
            while (current <= endDate.Date)
            {
                timeSlots.Add(new TimeSlot()
                {
                    Type = TimeSlotType.Daily,
                    SlotDate = current,
                    Space = space,
                    Tariff = tariff
                });
                current = current.AddDays(1);
            }
            return timeSlots;
        }

    }
}
