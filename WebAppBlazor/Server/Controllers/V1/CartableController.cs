using Application.Cartable.Commands.AddUser;
using Application.Cartable.Commands.CreateCartable;
using Application.Cartable.Commands.DeleteCartable;
using Application.Cartable.Commands.RemoveUser;
using Application.Cartable.Commands.UpdateCartable;
using Application.Cartable.Queries.GetCartableById;
using Application.Cartable.Queries.GetFilteredCartables;
using Application.Cartable.Queries.GetMyCartables;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CartableController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredCartablesQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }

        [HttpPost, Route("getMyCartables")]

        public async Task<IActionResult> GetAllMyCartable([FromBody] GetMyCartablesQuery dto)
        {
            var item = await Mediator.SendWithUow(dto);
            return Ok(item);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await Mediator.SendWithUow(new GetCartableByIdQuery() { Id = id });
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCartableCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCartableCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var item = await Mediator.SendWithUow(new DeleteCartableCommand() { Id = id });
            return Ok(item);
        }



        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("removeuser")]
        public async Task<IActionResult> RemoveUser([FromBody] RemoveUserCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }


    }
}
