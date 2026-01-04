using Application.Headers.Commands.CreateHeader;
using Application.Headers.Commands.DeleteHeader;
using Application.Headers.Commands.UpdateHeader;
using Application.Headers.Queries.GetFilteredHeaders;
using Application.Headers.Queries.GetHeader;
using Application_Backend.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class HeaderController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredHeadersQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Header = await Mediator.SendWithUow(new GetHeaderQuery() { Id = id });
            return Ok(Header);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateHeaderCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateHeaderCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var Header = await Mediator.SendWithUow(new DeleteHeaderCommand() { Id = id });
            return Ok(Header);
        }
    }
}
