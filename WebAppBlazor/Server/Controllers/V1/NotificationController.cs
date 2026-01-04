using Application.Notifications.Commands.StartNotification;
using Application.Notifications.Queries.GetNotificationStatus;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationController : ApiController
    {

        [HttpPost("StartStop")]
        public async Task<IActionResult> StartStop([FromBody] StarNotificationCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpGet("Status")]
        public async Task<IActionResult> GetStatus()
        {
            GetNotificationStatusQuery dto = new();
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }
    }
}
