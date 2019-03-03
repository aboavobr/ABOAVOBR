using Microsoft.AspNetCore.Mvc;

namespace aboavobr.raspberrypi.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
      [HttpGet("heartbeat")]
      public ActionResult GetHeartbeat()
      {
         return Ok();
      }
    }
}