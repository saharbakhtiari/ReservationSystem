using Application.Amenities.Queries.GetFilteredAmenities;
using Application.Amenitys.Commands.CreateAmenity;
using Application.Amenitys.Commands.DeleteAmenity;
using Application.Amenitys.Commands.UpdateAmenity;
using Application.Amenitys.Queries.GetAmenity;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AmenityController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredAmenitiesQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Amenity = await Mediator.SendWithUow(new GetAmenityByIdQuery() { Id = id });
            return Ok(Amenity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAmenityRequest model)
        {
            var dto = Mapper.Map<CreateAmenityCommand>(model);
            if (model.Icon != null)
            {
                using var ms = new MemoryStream();
                await model.Icon.CopyToAsync(ms);
                dto.Icon = new CreateAmenityFileCommand
                {
                    DataFiles = ms.ToArray(),
                    Name = model.Icon.FileName,
                    FileType = model.Icon.ContentType
                };

            }
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateAmenityRequest model)
        {
            var dto = Mapper.Map<UpdateAmenityCommand>(model);
            if (model.Icon != null)
            {
                using var ms = new MemoryStream();
                await model.Icon.CopyToAsync(ms);
                dto.Icon = new UpdateAmenityFileCommand
                {
                    Id = model.IconId,
                    DataFiles = ms.ToArray(),
                    Name = model.Icon.FileName,
                    FileType = model.Icon.ContentType
                };
            }
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var Amenity = await Mediator.SendWithUow(new DeleteAmenityCommand() { Id = id });
            return Ok(Amenity);
        }
    }
}
