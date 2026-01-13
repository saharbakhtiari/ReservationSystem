using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Tariffs.Commands.CreateTariff
{
    public class CreateTariffCommandValidator : AbstractValidator<CreateTariffCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateTariffCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
