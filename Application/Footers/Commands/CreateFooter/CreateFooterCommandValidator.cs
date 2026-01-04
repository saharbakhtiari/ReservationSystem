using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Footers.Commands.CreateFooter
{
    public class CreateFooterCommandValidator : AbstractValidator<CreateFooterCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateFooterCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
