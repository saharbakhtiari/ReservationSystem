using System;
using System.Collections.Generic;

namespace Application.Amenitys.Queries.GetAmenity
{
    public class GetAmenityByIdDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public GetAmenityByIdFileDto Icon { get; set; }
    }
    public class GetAmenityByIdFileDto
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }

}
