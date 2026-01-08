using Application.Spaces.Commands.CreateSpace;
using Domain.Contract.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.Spaces.Commands.UpdateSpace
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SpaceManager)]
    public class UpdateSpaceCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
        public List<long> AmenityIds { get; set; }
        public List<UpdateSpaceFileCommand> Images { get; set; }
    }

    public class UpdateSpaceFileCommand
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public int Order { get; set; }
    }

    public class UpdateSpaceRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } 
        public int Capacity { get; set; }
        public string Location { get; set; }
        public SpaceType Type { get; set; }
        public List<long> AmenityIds { get; set; }
        public long ImageId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
