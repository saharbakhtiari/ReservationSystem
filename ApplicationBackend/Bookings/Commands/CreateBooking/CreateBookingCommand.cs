using Application.Bookings.Commands.CreateBooking;
using AutoMapper;
using Domain.Bookings;
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
            var bookong = _mapper.Map<Booking>(request);
            await bookong.DomainService.SetTimeSlot(request.TimeSlotId, cancellationToken);
            await bookong.DomainService.SetProfile(cancellationToken);
            bookong.ConfirmedAt = DateTime.Now;
            await bookong.SaveAsync(cancellationToken);
            return bookong.Id;
        }
    }
}
