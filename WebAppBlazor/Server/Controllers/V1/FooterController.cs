using Application.Footers.Commands.CreateFooter;
using Application.Footers.Commands.DeleteFooter;
using Application.Footers.Commands.UpdateFooter;
using Application.Footers.Queries.GetFilteredFooters;
using Application.Footers.Queries.GetFooter;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class FooterController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredFootersQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Footer = await Mediator.SendWithUow(new GetFooterQuery() { Id = id });
            return Ok(Footer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateFooterCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateFooterCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var Footer = await Mediator.SendWithUow(new DeleteFooterCommand() { Id = id });
            return Ok(Footer);
        }
    }
}
