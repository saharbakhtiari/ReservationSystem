using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Spaces.Commands.DeleteSpace
{
    public class DeleteSpaceCommandValidator : AbstractValidator<DeleteSpaceCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteSpaceCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
