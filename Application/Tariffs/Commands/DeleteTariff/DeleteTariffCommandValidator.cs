using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Tariffs.Commands.DeleteTariff
{
    public class DeleteTariffCommandValidator : AbstractValidator<DeleteTariffCommand>
    {
        private readonly IStringLocalizer _localizer;
        public DeleteTariffCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
