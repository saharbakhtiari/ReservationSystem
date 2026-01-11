using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Amenitys.Commands.CreateAmenity
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_AmenityManager)]
    public class CreateAmenityCommand : IRequest<long>
    {
        public string Title { get; set; } = null!;
        public CreateAmenityFileCommand Icon { get; set; }
    }
    public class CreateAmenityFileCommand
    {
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }

    public class CreateAmenityRequest
    {
        public string Title { get; set; }
        public IFormFile Icon { get; set; }
    }
}
