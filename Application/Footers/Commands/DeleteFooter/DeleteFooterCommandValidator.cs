using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Footers.Commands.DeleteFooter
{
    public class DeleteFooterCommandValidator : AbstractValidator<DeleteFooterCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteFooterCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
