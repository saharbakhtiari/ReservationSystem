using Application.EducationCategories.Commands.PublishSlider;
using Application.Sliders.Commands.CreateSlider;
using Application.Sliders.Commands.DeleteSlider;
using Application.Sliders.Commands.UpdateSlider;
using Application.Sliders.Queries.GetFilteredSliders;
using Application.Sliders.Queries.GetSlider;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SliderController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredSlidersQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Slider = await Mediator.SendWithUow(new GetSliderQuery() { Id = id });
            return Ok(Slider);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSliderRequest model)
        {
            var dto = Mapper.Map<CreateSliderCommand>(model);
            if (model.Image != null)
            {
                using var ms = new MemoryStream();
                await model.Image.CopyToAsync(ms);
                dto.Image = new CreateSliderFileCommand
                {
                    DataFiles = ms.ToArray(),
                    Name = model.Image.FileName,
                    FileType = model.Image.ContentType
                };
            }
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSliderRequest model)
        {
            var dto = Mapper.Map<UpdateSliderCommand>(model);
            if (model.Image != null)
            {
                using var ms = new MemoryStream();
                await model.Image.CopyToAsync(ms);
                dto.Image = new UpdateSliderFileCommand
                {
                    Id = model.ImageId,
                    DataFiles = ms.ToArray(),
                    Name = model.Image.FileName,
                    FileType = model.Image.ContentType
                };
            }
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var Slider = await Mediator.SendWithUow(new DeleteSliderCommand() { Id = id });
            return Ok(Slider);
        }

        [HttpPost("publish")]
        public async Task<IActionResult> Publish([FromBody] PublishSliderCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }
    }
}
