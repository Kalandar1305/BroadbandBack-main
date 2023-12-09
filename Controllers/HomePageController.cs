using BroadBandBillingPaymentSystem.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace BroadBandBillingPaymentSystem.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private HomePageService _service;

        public HomePageController(HomePageService service)
        {
            _service = service;
        }

        [HttpGet("home-page")]
        public IActionResult GetHomePageData()
        {
            var data = _service.GetLandingPageData();
            return Ok(data);
        }
    }

}