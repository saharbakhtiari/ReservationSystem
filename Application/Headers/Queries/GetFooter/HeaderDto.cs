using Domain.Contract.Enums;

namespace Application.Headers.Queries.GetHeader
{
    public class HeaderDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte[] DataFiles { get; set; }
        public bool IsDraft { get; set; }
        public int Order { get; set; }
        public string Link { get; set; }
    }
}
