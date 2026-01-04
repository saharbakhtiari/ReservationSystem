using System;

namespace Application.SeoFiles.Queries.GetSeoFileById
{
    public class SeoFileDto
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
    }
}
