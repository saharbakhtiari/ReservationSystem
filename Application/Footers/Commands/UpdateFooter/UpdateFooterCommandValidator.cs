using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Footers.Commands.UpdateFooter
{
    public class UpdateFooterCommandValidator : AbstractValidator<UpdateFooterCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateFooterCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
