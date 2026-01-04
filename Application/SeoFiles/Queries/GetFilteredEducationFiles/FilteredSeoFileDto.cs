using System;

namespace Application.SeoFiles.Queries.GetFilteredSeoFiles
{
    public class FilteredSeoFileDto
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
    }
}
