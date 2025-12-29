namespace Application.Books.Queries.GetBookById
{
    public class BookDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; }
    }
}
