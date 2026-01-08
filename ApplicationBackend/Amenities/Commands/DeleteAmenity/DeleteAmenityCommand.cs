using Application.Amenitys.Commands.DeleteAmenity;
using Domain.Amenitys;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Amenitys.Commands.DeleteAmenity;

public class DeleteAmenityCommandHandler : IRequestHandler<DeleteAmenityCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteAmenityCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = await Amenity.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Amenity not found"]);
        await amenity.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
