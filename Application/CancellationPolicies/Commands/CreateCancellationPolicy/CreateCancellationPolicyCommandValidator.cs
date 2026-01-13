using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.CancellationPolicys.Commands.CreateCancellationPolicy
{
    public class CreateCancellationPolicyCommandValidator : AbstractValidator<CreateCancellationPolicyCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateCancellationPolicyCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
