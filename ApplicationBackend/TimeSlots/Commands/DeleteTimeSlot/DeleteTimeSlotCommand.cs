using Application.TimeSlots.Commands.DeleteTimeSlot;
using Domain.TimeSlots;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.TimeSlots.Commands.DeleteTimeSlot;

public class DeleteTimeSlotCommandHandler : IRequestHandler<DeleteTimeSlotCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteTimeSlotCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var timeSlot = await TimeSlot.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["TimeSlot not found"]);
        timeSlot.IsDeleted = true;
        await timeSlot.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
