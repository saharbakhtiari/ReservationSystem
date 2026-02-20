using Application.Bookings.Commands.RevokeBooking;
using Domain.Bookings;
using Domain.Contract.Enums;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Commands.RevokeBooking;

public class RevokeBookingCommandHandler : IRequestHandler<RevokeBookingCommand>
{
    private readonly IStringLocalizer _localizer;


    public RevokeBookingCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(RevokeBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await Booking.GetAsync(request.Id, false, cancellationToken) ?? throw new UserFriendlyException(_localizer["Booking not found"]);
        if(booking.Status == BookingStatus.UserRevoked || booking.Status == BookingStatus.AdminRevoked)
        {
            throw new UserFriendlyException(_localizer["Bookig has already been revoked"]);
        }
        booking.Status = BookingStatus.UserRevoked;
        await booking.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
