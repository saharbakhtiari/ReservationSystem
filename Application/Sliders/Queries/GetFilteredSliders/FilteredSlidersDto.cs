using Application.Sliders.Commands.CreateSlider;
using Domain.Contract.Enums;
using System;

namespace Application.Sliders.Queries.GetFilteredSliders
{
    public class FilteredSlidersDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public FilteredSliderFileDto Image { get; set; }
        public int Order { get; set; }
        public string Link { get; set; }
        public SliderType Type { get; set; }
    }

    public class FilteredSliderFileDto
    {
        public Guid FileGuid { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
    }
}
