using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Tariffs.Commands.UpdateTariff
{
    public class UpdateTariffCommandValidator : AbstractValidator<UpdateTariffCommand>
    {
        private readonly IStringLocalizer _localizer;
        public UpdateTariffCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Id).GreaterThan(0).WithMessage(_localizer["Id is not valid"]);
        }
    }
}
