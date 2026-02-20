using Application.Bookings.Commands.AdminRevokeBooking;
using Domain.Bookings;
using Domain.Contract.Enums;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Commands.AdminRevokeBooking;

public class AdminRevokeBookingCommandHandler : IRequestHandler<AdminRevokeBookingCommand>
{
    private readonly IStringLocalizer _localizer;


    public AdminRevokeBookingCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(AdminRevokeBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await Booking.GetAsync(request.Id, true, cancellationToken) ?? throw new UserFriendlyException(_localizer["Booking not found"]);
        if (booking.Status == BookingStatus.UserRevoked || booking.Status == BookingStatus.AdminRevoked)
        {
            throw new UserFriendlyException(_localizer["Bookig has already been revoked"]);
        }
        booking.Status = BookingStatus.AdminRevoked;
        await booking.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
