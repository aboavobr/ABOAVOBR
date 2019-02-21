using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace aboavobr_raspberrypi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class DebugController : ControllerBase
   {
      // GET api/debug
      [HttpGet]
      public ActionResult<IEnumerable<string>> Get()
      {
         return new string[] { "It's alive" };
      }
   }
}