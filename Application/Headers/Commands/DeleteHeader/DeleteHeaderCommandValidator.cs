using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Headers.Commands.DeleteHeader
{
    public class DeleteHeaderCommandValidator : AbstractValidator<DeleteHeaderCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteHeaderCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
