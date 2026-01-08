using Domain.Contract.Enums;
using Domain.Footers;
using System.Collections.Generic;

namespace Application.Footers.Queries.GetFooter
{
    public class FooterDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Order { get; set; }
        public List<FootersLinkDto> Links { get; set; }
    }
    public class FootersLinkDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
