using Application.Headers.Commands.DeleteHeader;
using Domain.Headers;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Headers.Commands.DeleteHeader;

public class DeleteHeaderCommandHandler : IRequestHandler<DeleteHeaderCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteHeaderCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteHeaderCommand request, CancellationToken cancellationToken)
    {
        var header = await Header.GetHeaderAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Header not found"]);
        header.IsDeleted = true;
        await header.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
