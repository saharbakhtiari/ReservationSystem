using Application.Spaces.Commands.DeleteSpace;
using Domain.Spaces;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Spaces.Commands.DeleteSpace;

public class DeleteSpaceCommandHandler : IRequestHandler<DeleteSpaceCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteSpaceCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await Space.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Space not found"]);
        space.Amenities = null;
        space.IsDeleted = true;
        await space.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
