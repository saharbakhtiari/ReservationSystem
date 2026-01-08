using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Amenitys.Commands.CreateAmenity
{
    public class CreateAmenityCommandValidator : AbstractValidator<CreateAmenityCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateAmenityCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
          //  RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            // RuleFor(p => p.DataFiles).NotEmpty().WithMessage("فایل ارسالی صحیح نیست");
        }
    }
}
