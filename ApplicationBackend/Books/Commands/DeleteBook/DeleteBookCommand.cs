using Application.Books.Commands.DeleteBook;
using Domain.Books;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IStringLocalizer _localizer;

    public DeleteBookCommandHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await Book.GetAsync(request.Id, cancellationToken) ?? throw new UserFriendlyException(_localizer["Record not found"]);
        book.IsDeleted = true;
        await book.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}
