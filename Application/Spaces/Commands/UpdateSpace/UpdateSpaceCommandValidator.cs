using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Spaces.Commands.UpdateSpace
{
    public class UpdateSpaceCommandValidator : AbstractValidator<UpdateSpaceCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateSpaceCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
