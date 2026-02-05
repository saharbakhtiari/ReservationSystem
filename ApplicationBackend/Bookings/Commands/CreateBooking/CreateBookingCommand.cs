using Application.Bookings.Commands.CreateBooking;
using AutoMapper;
using Domain.Bookings;
using Extensions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, long>
    {
        private readonly IMapper _mapper;


        public CreateBookingCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request);
            await booking.DomainService.SetProfile(cancellationToken);
            await booking.DomainService.SetTimeSlot(request.TimeSlotId, cancellationToken);
            booking.ConfirmedAt = DateTime.Now;
            booking.PriceSnapshot = booking.TimeSlot.Tariff.ToJson();
           // booking.PolicySnapshot = booking.TimeSlot.Space.ToJson();
            await booking.SaveAsync(cancellationToken);
            return booking.Id;
        }
    }
}
