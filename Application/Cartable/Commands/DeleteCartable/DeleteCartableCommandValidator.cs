using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Cartable.Commands.DeleteCartable
{
    public class DeleteCartableCommandValidator : AbstractValidator<DeleteCartableCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteCartableCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
