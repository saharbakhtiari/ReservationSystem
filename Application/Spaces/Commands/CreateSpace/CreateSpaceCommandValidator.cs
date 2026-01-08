using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Spaces.Commands.CreateSpace
{
    public class CreateSpaceCommandValidator : AbstractValidator<CreateSpaceCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateSpaceCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
