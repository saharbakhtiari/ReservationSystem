using Application.Amenities.Queries.GetFilteredAmenities;
using Application.Bookings.Commands.CreateBooking;
using Application.Bookings.Commands.DeleteBooking;
using Application.Bookings.Commands.UpdateBooking;
using Application.Bookings.Queries.GetFilteredBookings;
using Application.Bookings.Queries.GetBooking;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Bookings.Queries.AdminGetFilteredBookings;
using Application.Bookings.Queries.AdminGetBooking;
using Application.Bookings.Commands.RevokeBooking;
using Application.Bookings.Commands.AdminRevokeBooking;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BookingController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredBookingsQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Booking = await Mediator.SendWithUow(new GetBookingByIdQuery() { Id = id });
            return Ok(Booking);
        }

        [HttpPost("adminsearch")]
        public async Task<IActionResult> GetFiltered([FromBody] AdminGetFilteredBookingsQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet("adminget")]
        public async Task<IActionResult> AdminGetById(long id)
        {
            var Booking = await Mediator.SendWithUow(new AdminGetBookingByIdQuery() { Id = id });
            return Ok(Booking);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RevokeBookingCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("adminrevoke")]
        public async Task<IActionResult> AdminRevoke([FromBody] AdminRevokeBookingCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateBookingCommand dto)
        //{
        //    var id = await Mediator.SendWithUow(dto);
        //    return Ok(id);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateBookingCommand dto)
        //{
        //    await Mediator.SendWithUow(dto);
        //    return Ok(true);
        //}

        //[HttpDelete, Route("{Id}")]
        //public async Task<IActionResult> DeleteById(long id)
        //{
        //    var Booking = await Mediator.SendWithUow(new DeleteBookingCommand() { Id = id });
        //    return Ok(Booking);
        //}
    }
}
