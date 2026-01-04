using Application.Cartable.Commands.DeleteCartable;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Cartable.Commands.DeleteCartable;

public class DeleteCartableCommandHandler : IRequestHandler<DeleteCartableCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteCartableCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteCartableCommand request, CancellationToken cancellationToken)
    {
        var Cartable = await Domain.Cartables.Cartable.GetCartableByIdAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Record not found"]);
        Cartable.IsDeleted = true;
        await Cartable.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
