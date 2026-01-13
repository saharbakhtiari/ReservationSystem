using Application.Tariffs.Commands.DeleteTariff;
using Domain.Tariffs;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Tariffs.Commands.DeleteTariff;

public class DeleteTariffCommandHandler : IRequestHandler<DeleteTariffCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteTariffCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteTariffCommand request, CancellationToken cancellationToken)
    {
        var tariff = await Tariff.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Tariff not found"]);
        await tariff.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
