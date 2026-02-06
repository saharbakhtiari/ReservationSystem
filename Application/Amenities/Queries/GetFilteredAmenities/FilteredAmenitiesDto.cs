using Domain.Contract.Enums;
using System;
using System.Collections.Generic;

namespace Application.Amenities.Queries.GetFilteredAmenities
{
    public class FilteredAmenitiesDto
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public FilteredAmenitiesFileDto Icon { get; set; }
    }

    public class FilteredAmenitiesFileDto
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }
}
