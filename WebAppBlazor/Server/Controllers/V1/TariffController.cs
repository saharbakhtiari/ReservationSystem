using Application.Amenities.Queries.GetFilteredAmenities;
using Application.Tariffs.Commands.CreateTariff;
using Application.Tariffs.Commands.DeleteTariff;
using Application.Tariffs.Commands.UpdateTariff;
using Application.Tariffs.Queries.GetFilteredTariffs;
using Application.Tariffs.Queries.GetTariff;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TariffController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredTariffsQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Tariff = await Mediator.SendWithUow(new GetTariffByIdQuery() { Id = id });
            return Ok(Tariff);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTariffCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTariffCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var Tariff = await Mediator.SendWithUow(new DeleteTariffCommand() { Id = id });
            return Ok(Tariff);
        }
    }
}
