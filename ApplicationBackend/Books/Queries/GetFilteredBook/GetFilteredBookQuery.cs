using Application.Books.Queries.GetFilteredBooks;
using Domain.Books;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Backend.Books.Queries.GetFilteredBook
{
    public class GetFilteredBookQueryHandler : IRequestHandler<GetFilteredBooksQuery, PagedList<FilteredBooksDto>>
    {
        public Task<PagedList<FilteredBooksDto>> Handle(GetFilteredBooksQuery request, CancellationToken cancellationToken)
        {
            return new Book().Repository.GetFilteredAsync<FilteredBooksDto>(request.Filter, request.Sort, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
