using Application.Bookings.Commands.UpdateBooking;
using AutoMapper;
using Domain.Bookings;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;


        public UpdateBookingCommandHandler(IMapper mapper, IStringLocalizer localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var tariff = await Booking.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            _mapper.Map(request, tariff);
            await tariff.DomainService.SetTimeSlot(request.TimeSlotId, cancellationToken);
            await tariff.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
