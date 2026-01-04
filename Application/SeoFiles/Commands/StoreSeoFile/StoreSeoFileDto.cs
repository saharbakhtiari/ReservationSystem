using System;

namespace Application.SeoFiles.Commands.StoreSeoFile
{
    public class StoreSeoFileDto
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Url { get; set; }
    }
}
