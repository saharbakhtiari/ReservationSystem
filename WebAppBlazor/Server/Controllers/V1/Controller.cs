using Microsoft.AspNetCore.Mvc;

namespace WebAppBlazor.Server.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Controller : ApiController
    {
        //[HttpPost("chequeanddebtinquirys")]
        //public async Task<IActionResult> ChequeAndDebtInquirys(GetChequeAndDebtInquiryQuery query)
        //{
        //    //ToDo
        //    var output = await Mediator.SendWithUow(query);
        //    return Ok(output);
        //}
    }
}
