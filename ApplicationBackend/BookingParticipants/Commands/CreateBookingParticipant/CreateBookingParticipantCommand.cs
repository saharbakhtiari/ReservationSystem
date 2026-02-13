using Application.BookingParticipants.Commands.CreateBookingParticipant;
using AutoMapper;
using Domain.BookingParticipants;
using Domain.Bookings;
using Exceptions;
using Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingParticipants.Commands.CreateBookingParticipant
{
    public class CreateBookingParticipantCommandHandler : IRequestHandler<CreateBookingParticipantCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;


        public CreateBookingParticipantCommandHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<long> Handle(CreateBookingParticipantCommand request, CancellationToken cancellationToken)
        {
            var participant = _mapper.Map<BookingParticipant>(request);
            var booking = await Booking.GetAsync(request.BookingId, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            participant.Booking = booking;
            await participant.SaveAsync(cancellationToken);
            return participant.Id;
        }
    }
}
