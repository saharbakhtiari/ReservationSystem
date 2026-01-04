using Application.SeoFiles.Commands.DeleteSeoFile;
using Domain.SeoFiles;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.SeoFiles.Commands.DeleteSeoFile;

public class DeleteSeoFileCommandHandler : IRequestHandler<DeleteSeoFileCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteSeoFileCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteSeoFileCommand request, CancellationToken cancellationToken)
    {
        var file = await SeoFile.GetFileAsync(request.FileGuid, cancellationToken) ?? throw new UserFriendlyException(_localizer["File not found"]);
        file.IsDeleted = true;
        await file.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
