using Domain.Common;
using Domain.Contract.Enums;
using Domain.Spaces;
using Domain.Tariffs;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.TimeSlots
{
    public class TimeSlot : AuditableEntity
    {
        public Space Space { get; set; } = null!;
        public Tariff Tariff { get; set; } = null!;
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public DateTime SlotDate { get; set; }
        [JsonIgnore]
        public bool IsBooked { get; set; }
        public int AvailableCount { get; set; }
        public TimeSlotType Type { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public ITimeSlotDomainService DomainService { get; set; }
        [JsonIgnore]
        public ITimeSlotRepository Repository { get; set; }

        public TimeSlot()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<ITimeSlotDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<ITimeSlotRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<TimeSlot> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ITimeSlotRepository>();
            var item = await repository.GetAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }

        public static async Task<TimeSlot> GetIncludedAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<ITimeSlotRepository>();
            var item = await repository.GetIncludedAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
