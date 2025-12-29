using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQuery : IRequest<BookDto>
    {
        public long Id { get; set; }
    }
}
