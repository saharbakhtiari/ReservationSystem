using System;
using System.Collections.Generic;

namespace Application.Books.Queries.GetFilteredBooks
{
    public class FilteredBooksDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
