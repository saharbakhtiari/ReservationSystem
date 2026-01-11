using Application.Spaces.Commands.UpdateSpace;
using Domain.Contract.Enums;
using Domain.SpaceFiles;
using Domain.Spaces;
using System;
using System.Collections.Generic;

namespace Application.Spaces.Queries.GetSpace
{
    public class GetSpaceByIdDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
        public List<GetSpaceByIdAmenity> Amenities { get; set; }
        public List<GetSpaceByIdFileDto> Gallery { get; set; }
        public GetSpaceByIdFileDto MainImage { get; set; }
    }
    public class GetSpaceByIdFileDto
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public int Order { get; set; }
    }

    public class GetSpaceByIdAmenity
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public GetSpaceByIdFileDto Icon { get; set; }
    }
}
