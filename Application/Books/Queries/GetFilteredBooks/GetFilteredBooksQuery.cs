using Domain.Common;
using MediatR;
using System;

namespace Application.Books.Queries.GetFilteredBooks
{
    public class GetFilteredBooksQuery : IRequest<PagedList<FilteredBooksDto>>
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
 
}
