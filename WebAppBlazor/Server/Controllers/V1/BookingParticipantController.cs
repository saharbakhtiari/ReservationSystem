using Application.BookingParticipants.Commands.CreateBookingParticipant;
using Application.BookingParticipants.Queries.AdminGetBookingParticipant;
using Application.BookingParticipants.Queries.AdminGetFilteredBookingParticipants;
using Application.BookingParticipants.Queries.GetBookingParticipant;
using Application.BookingParticipants.Queries.GetFilteredBookingParticipants;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BookingParticipantController : ApiController
    {


        //[HttpPost("search")]
        //public async Task<IActionResult> GetFiltered([FromBody] GetFilteredBookingParticipantsQuery dto)
        //{
        //    var output = await Mediator.SendWithUow(dto);
        //    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
        //    return Ok(output);
        //}
        //[HttpGet, Route("{id}")]
        //public async Task<IActionResult> GetById(long id)
        //{
        //    var BookingParticipant = await Mediator.SendWithUow(new GetBookingParticipantByIdQuery() { Id = id });
        //    return Ok(BookingParticipant);
        //}

        //[HttpPost("adminsearch")]
        //public async Task<IActionResult> GetFiltered([FromBody] AdminGetFilteredBookingParticipantsQuery dto)
        //{
        //    var output = await Mediator.SendWithUow(dto);
        //    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
        //    return Ok(output);
        //}
        //[HttpGet("adminget")]
        //public async Task<IActionResult> AdminGetById(long id)
        //{
        //    var BookingParticipant = await Mediator.SendWithUow(new AdminGetBookingParticipantByIdQuery() { Id = id });
        //    return Ok(BookingParticipant);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingParticipantCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateBookingParticipantCommand dto)
        //{
        //    await Mediator.SendWithUow(dto);
        //    return Ok(true);
        //}

        //[HttpDelete, Route("{Id}")]
        //public async Task<IActionResult> DeleteById(long id)
        //{
        //    var BookingParticipant = await Mediator.SendWithUow(new DeleteBookingParticipantCommand() { Id = id });
        //    return Ok(BookingParticipant);
        //}
    }
}
