using Application.Footers.Commands.DeleteFooter;
using Domain.Footers;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Footers.Commands.DeleteFooter;

public class DeleteFooterCommandHandler : IRequestHandler<DeleteFooterCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteFooterCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteFooterCommand request, CancellationToken cancellationToken)
    {
        var footer = await Footer.GetFooterAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Footer not found"]);
        footer.IsDeleted = true;
        await footer.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
