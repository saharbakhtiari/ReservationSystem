using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAppBlazor.Server.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CaptchaController : ApiController
    {
        private readonly IDNTCaptchaApiProvider _apiProvider;

        public CaptchaController(IDNTCaptchaApiProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
        [EnableRateLimiting(DNTCaptchaRateLimiterPolicy.Name)] // don't forget this one!
        public ActionResult<DNTCaptchaApiResponse> CreateDNTCaptchaParams() =>
         // Note: For security reasons, a JavaScript client shouldn't be able to provide these attributes directly.
         // Otherwise an attacker will be able to change them and make them easier!
         _apiProvider.CreateDNTCaptcha(new DNTCaptchaTagHelperHtmlAttributes
         {
             BackColor = "#fffff",
             FontName = "Tahoma",
             FontSize = 42,
             ForeColor = "#000000",

             Language = Language.Persian,
             DisplayMode = DisplayMode.ShowDigits,
             Max = 9999,
             Min = 1000,
             UseRelativeUrls = true
         });
    }
}
