using Application.Spaces.Commands.CreateSpace;
using Application.Spaces.Commands.DeleteSpace;
using Application.Spaces.Commands.UpdateSpace;
using Application.Spaces.Queries.GetFilteredSpaces;
using Application.Spaces.Queries.GetSpace;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SpaceController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredSpacesQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Space = await Mediator.SendWithUow(new GetSpaceByIdQuery() { Id = id });
            return Ok(Space);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSpaceRequest model)
        {
            var dto = Mapper.Map<CreateSpaceCommand>(model);
            if (model.Images != null)
            {
                foreach(var img in model.Images)
                {
                    using var ms = new MemoryStream();
                    await img.CopyToAsync(ms);
                    dto.Images.Add(new CreateSpaceFileCommand
                    {
                        DataFiles = ms.ToArray(),
                        Name = img.FileName,
                        FileType = img.ContentType
                    });
                }
            }
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSpaceRequest model)
        {
            var dto = Mapper.Map<UpdateSpaceCommand>(model);
            if (model.Images != null)
            {
                foreach(var img in model.Images)
                {
                    using var ms = new MemoryStream();
                    await img.CopyToAsync(ms);
                    dto.Images.Add(new UpdateSpaceFileCommand
                    {
                        Id = model.ImageId,
                        DataFiles = ms.ToArray(),
                        Name = img.FileName,
                        FileType = img.ContentType
                    });
                }
            }
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var Space = await Mediator.SendWithUow(new DeleteSpaceCommand() { Id = id });
            return Ok(Space);
        }
    }
}
