using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        private readonly IStringLocalizer _localizer;
        public CreateBookCommandValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            RuleFor(p => p.Title).NotEmpty().WithMessage(_localizer["Title is empty"]);
            RuleFor(p => p.Image).NotEmpty().WithMessage(_localizer["Body is empty"]);
        }
    }
}
