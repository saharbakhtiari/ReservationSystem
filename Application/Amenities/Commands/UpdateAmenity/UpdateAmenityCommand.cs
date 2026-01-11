using Application.Amenitys.Commands.CreateAmenity;
using Domain.Contract.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.Amenitys.Commands.UpdateAmenity
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_AmenityManager)]
    public class UpdateAmenityCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public UpdateAmenityFileCommand Icon { get; set; }
    }

    public class UpdateAmenityFileCommand
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }

    public class UpdateAmenityRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } 
        public long IconId { get; set; }
        public IFormFile Icon { get; set; }
    }
}
