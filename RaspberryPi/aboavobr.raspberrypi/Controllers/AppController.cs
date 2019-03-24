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

      /// <summary>
      /// Gets the battery life in percentage.
      /// </summary>
      /// <returns>The battery state in percentage</returns>
      [HttpGet("battery")]
      public ActionResult<int> GetBatteryLife()
      {
         return new ActionResult<int>(serialCommunicaitonService.GetBatteryLife());
      }

      [HttpPost("move")]
      public ActionResult PostMoveCommand([FromBody]Direction direction)
      {
         serialCommunicaitonService.Move(direction);
         return Ok();
      }
   }
}