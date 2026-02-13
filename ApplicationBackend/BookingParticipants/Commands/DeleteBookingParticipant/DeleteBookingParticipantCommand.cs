using Application.BookingParticipants.Commands.DeleteBookingParticipant;
using Domain.BookingParticipants;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.BookingParticipants.Commands.DeleteBookingParticipant;

public class DeleteBookingParticipantCommandHandler : IRequestHandler<DeleteBookingParticipantCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteBookingParticipantCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteBookingParticipantCommand request, CancellationToken cancellationToken)
    {
        var booking = await BookingParticipant.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["BookingParticipant not found"]);
        booking.IsDeleted = true;
        await booking.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
