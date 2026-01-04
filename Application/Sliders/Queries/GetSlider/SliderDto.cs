using Application.Sliders.Queries.GetFilteredSliders;
using Domain.Contract.Enums;
using System;

namespace Application.Sliders.Queries.GetSlider
{
    public class SliderDto
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public GetSliderFileDto Image { get; set; }
        public int Order { get; set; }
        public string Link { get; set; }
        public SliderType Type { get; set; }
    }

    public class GetSliderFileDto
    {
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }
}
