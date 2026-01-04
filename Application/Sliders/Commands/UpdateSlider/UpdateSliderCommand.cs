using Application.Common.Security;
using Domain.Contract.Enums;
using Domain.Permissions;
using Domain.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Sliders.Commands.UpdateSlider
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SliderManager)]
    public class UpdateSliderCommand : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Body { get; set; }
        public UpdateSliderFileCommand Image { get; set; }
        public int Order { get; set; }
        public SliderType Type { get; set; }
    }

    public class UpdateSliderFileCommand
    {
        public long Id { get; set; }
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }

    public class UpdateSliderRequest : IRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        public long ImageId { get; set; }
        public int Order { get; set; }
        public SliderType Type { get; set; }
    }
}
