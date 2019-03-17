using aboavobr.raspberrypi.Services;
using Microsoft.AspNetCore.Mvc;

namespace aboavobr.raspberrypi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ISerialCommunicationService serialCommunicaitonService;

        public AppController(ISerialCommunicationService serialCommunicaitonService)
        {
            this.serialCommunicaitonService = serialCommunicaitonService;
        }

        [HttpGet("heartbeat")]
        public ActionResult GetHeartbeat()
        {
            return Ok();
        }

        [HttpGet("battery")]
        public ActionResult<int> GetBatteryLife()
        {
            return new ActionResult<int>(serialCommunicaitonService.GetBatteryLife());
        }
    }
}