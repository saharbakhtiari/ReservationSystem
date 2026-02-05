using Application.Bookings.Commands.DeleteBooking;
using Domain.Bookings;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Bookings.Commands.DeleteBooking;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteBookingCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await Booking.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Booking not found"]);
        booking.IsDeleted = true;
        await booking.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
