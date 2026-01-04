using Application.SmsNotifications.Commands.CheckSmsNotification;
using Application.SmsNotifications.Commands.CreateSmsNotification;
using Application.SmsNotifications.Commands.DeleteSmsNotification;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SmsNotificationController : ApiController
    {

        [HttpPost("createnotification")]
        public async Task<IActionResult> CreateNotification([FromBody] CreateRuleSmsNotifyCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPost("checknotify")]
        public async Task<IActionResult> CheckNotify([FromBody] CheckRuleSmsNotifyCommand dto)
        {
            var result = await Mediator.SendWithUow(dto);
            return Ok(result);
        }

        [HttpPost("deletenotification")]
        public async Task<IActionResult> DeleteNotification([FromBody] DeleteRuleSmsNotifyCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }
    }
}
