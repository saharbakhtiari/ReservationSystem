using Domain.Footers;
using System.Collections.Generic;

namespace Application.Footers.Queries.GetFilteredFooters
{
    public class FilteredFootersDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Order { get; set; }
        public List<FooterLink> Links { get; set; }
    }

    public class FilteredFootersLinkDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
