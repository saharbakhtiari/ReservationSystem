using Application.EducationCategories.Commands.PublishSlider;
using Domain.Sliders;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Departments.Commands.PublishSlider
{
    public class PublishSliderCommandHandler : IRequestHandler<PublishSliderCommand>
    {
        private readonly IStringLocalizer _localizer;

        public PublishSliderCommandHandler(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public async Task<Unit> Handle(PublishSliderCommand request, CancellationToken cancellationToken)
        {
            var item = await Slider.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Item not found"]);
            item.IsPublished = request.IsPublished;
            await item.SaveAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
