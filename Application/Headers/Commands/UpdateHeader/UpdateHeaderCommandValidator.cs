using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Headers.Commands.UpdateHeader
{
    public class UpdateHeaderCommandValidator : AbstractValidator<UpdateHeaderCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateHeaderCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
