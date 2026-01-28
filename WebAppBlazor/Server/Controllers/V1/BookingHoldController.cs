using Application.Amenities.Queries.GetFilteredAmenities;
using Application.BookingHolds.Commands.CreateBookingHold;
using Application.BookingHolds.Commands.DeleteBookingHold;
using Application.BookingHolds.Commands.UpdateBookingHold;
using Application.BookingHolds.Queries.GetFilteredBookingHolds;
using Application.BookingHolds.Queries.GetBookingHold;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BookingHoldController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredBookingHoldsQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var bookingHold = await Mediator.SendWithUow(new GetBookingHoldByIdQuery() { Id = id });
            return Ok(bookingHold);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingHoldCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateBookingHoldCommand dto)
        //{
        //    await Mediator.SendWithUow(dto);
        //    return Ok(true);
        //}

        //[HttpDelete, Route("{Id}")]
        //public async Task<IActionResult> DeleteById(long id)
        //{
        //    var BookingHold = await Mediator.SendWithUow(new DeleteBookingHoldCommand() { Id = id });
        //    return Ok(BookingHold);
        //}
    }
}
