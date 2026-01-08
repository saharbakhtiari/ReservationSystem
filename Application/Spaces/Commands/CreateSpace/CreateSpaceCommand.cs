using Domain.Amenitys;
using Domain.Contract.Enums;
using Domain.SpaceFiles;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.Spaces.Commands.CreateSpace
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SpaceManager)]
    public class CreateSpaceCommand : IRequest<long>
    {
        public string Title { get; set; } = null!;
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
        public List<long> AmenityIds { get; set; }
        public List<CreateSpaceFileCommand> Images { get; set; }
    }
    public class CreateSpaceFileCommand
    {
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public int Order { get; set; }
    }

    public class CreateSpaceRequest
    {
        public string Title { get; set; } 
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
        public List<long> AmenityIds { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
