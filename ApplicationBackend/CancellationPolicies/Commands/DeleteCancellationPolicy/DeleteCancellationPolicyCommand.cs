using Application.CancellationPolicys.Commands.DeleteCancellationPolicy;
using Domain.CancellationPolicys;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.CancellationPolicys.Commands.DeleteCancellationPolicy;

public class DeleteCancellationPolicyCommandHandler : IRequestHandler<DeleteCancellationPolicyCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteCancellationPolicyCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteCancellationPolicyCommand request, CancellationToken cancellationToken)
    {
        var tariff = await CancellationPolicy.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["CancellationPolicy not found"]);
        await tariff.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
