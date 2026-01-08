using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Amenitys.Commands.DeleteAmenity
{
    public class DeleteAmenityCommandValidator : AbstractValidator<DeleteAmenityCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteAmenityCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
