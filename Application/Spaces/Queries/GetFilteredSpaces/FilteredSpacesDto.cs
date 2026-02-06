using Domain.Amenitys;
using Domain.Contract.Enums;
using Domain.SpaceFiles;
using Domain.Spaces;
using System;
using System.Collections.Generic;

namespace Application.Spaces.Queries.GetFilteredSpaces
{
    public class FilteredSpacesDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
        public FilteredSpacesFileDto MainImage { get; set; }
        //public List<FilteredSpacesFileDto> Gallery { get; set; }
    }

    public class FilteredSpacesFileDto
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }
}
