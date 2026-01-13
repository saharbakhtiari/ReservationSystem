using Application.CancellationPolicys.Commands.CreateCancellationPolicy;
using Application.CancellationPolicys.Commands.DeleteCancellationPolicy;
using Application.CancellationPolicys.Commands.UpdateCancellationPolicy;
using Application.CancellationPolicys.Queries.GetCancellationPolicy;
using Application.CancellationPolicys.Queries.GetFilteredCancellationPolicys;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CancellationPolicyController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredCancellationPolicysQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var CancellationPolicy = await Mediator.SendWithUow(new GetCancellationPolicyByIdQuery() { Id = id });
            return Ok(CancellationPolicy);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCancellationPolicyCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCancellationPolicyCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var CancellationPolicy = await Mediator.SendWithUow(new DeleteCancellationPolicyCommand() { Id = id });
            return Ok(CancellationPolicy);
        }
    }
}
