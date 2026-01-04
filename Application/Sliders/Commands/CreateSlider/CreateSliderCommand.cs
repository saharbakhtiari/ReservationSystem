using Domain.Contract.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Sliders.Commands.CreateSlider
{
    //[Authorize(Roles = DefaultRoleNames.Admin, Permissions = PermissionNames.Manager_SliderManager)]
    public class CreateSliderCommand : IRequest<long>
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Body { get; set; }
        public CreateSliderFileCommand Image { get; set; }
        public int Order { get; set; }
        public SliderType Type { get; set; }
    }

    public class CreateSliderFileCommand
    {
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }
    public class CreateSliderRequest
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        public int Order { get; set; }
        public SliderType Type { get; set; }
    }
}
