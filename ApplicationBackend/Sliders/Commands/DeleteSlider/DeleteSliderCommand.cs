using Application.Sliders.Commands.DeleteSlider;
using Domain.Sliders;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Sliders.Commands.DeleteSlider;

public class DeleteSliderCommandHandler : IRequestHandler<DeleteSliderCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteSliderCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await Slider.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Slider not found"]);
        slider.IsDeleted = true;
        await slider.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
