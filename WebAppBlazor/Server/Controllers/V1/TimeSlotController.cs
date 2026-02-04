using Application.Amenities.Queries.GetFilteredAmenities;
using Application.TimeSlots.Commands.CreateTimeSlot;
using Application.TimeSlots.Commands.DeleteTimeSlot;
using Application.TimeSlots.Commands.UpdateTimeSlot;
using Application.TimeSlots.Queries.GetFilteredTimeSlots;
using Application.TimeSlots.Queries.GetTimeSlot;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TimeSlotController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredTimeSlotsQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var TimeSlot = await Mediator.SendWithUow(new GetTimeSlotByIdQuery() { Id = id });
            return Ok(TimeSlot);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTimeSlotCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateTimeSlotCommand dto)
        //{
        //    await Mediator.SendWithUow(dto);
        //    return Ok(true);
        //}

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var TimeSlot = await Mediator.SendWithUow(new DeleteTimeSlotCommand() { Id = id });
            return Ok(TimeSlot);
        }
    }
}
