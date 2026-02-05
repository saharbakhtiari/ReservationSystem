using Application.BookingHolds.Commands.DeleteBookingHold;
using Domain.BookingHolds;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingHolds.Commands.DeleteBookingHold;

public class DeleteBookingHoldCommandHandler : IRequestHandler<DeleteBookingHoldCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteBookingHoldCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteBookingHoldCommand request, CancellationToken cancellationToken)
    {
        var hold = await BookingHold.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["BookingHold not found"]);
        await hold.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
