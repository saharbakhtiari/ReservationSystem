using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Amenitys.Commands.UpdateAmenity
{
    public class UpdateAmenityCommandValidator : AbstractValidator<UpdateAmenityCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateAmenityCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
