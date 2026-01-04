using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Headers.Commands.CreateHeader
{
    public class CreateHeaderCommandValidator : AbstractValidator<CreateHeaderCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateHeaderCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
