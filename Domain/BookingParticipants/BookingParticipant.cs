using Domain.Amenitys;
using Domain.Common;
using Domain.Contract.Enums;
using Domain.MemberProfiles;
using Domain.SpaceFiles;
using Domain.Spaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.BookingParticipants
{
    public class BookingParticipant : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public IBookingParticipantDomainService DomainService { get; set; }
        public IBookingParticipantRepository Repository { get; set; }

        public BookingParticipant()
        {
            DomainService = ServiceLocator.ServiceProvider.GetService<IBookingParticipantDomainService>();
            Repository = ServiceLocator.ServiceProvider.GetService<IBookingParticipantRepository>();
            DomainService.OwnerEntity = this;
            Repository.OwnerEntity = this;
        }

        public override async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Repository.SaveAsync(cancellationToken);
        }

        public static async Task<BookingParticipant> GetAsync(long id, CancellationToken cancellationToken)
        {
            var repository = ServiceLocator.ServiceProvider.GetService<IBookingParticipantRepository>();
            var item = await repository.GetAsync(id, cancellationToken);
            if (item is not null)
            {
                item.Repository = repository;
                item.Repository.OwnerEntity = item;
            }
            return item;
        }
    }
}
