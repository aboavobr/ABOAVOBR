using System.Threading.Tasks;
using aboavobr.raspberrypi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aboavobr.raspberrypi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CameraController : ControllerBase
   {
      private readonly ILogger<CameraController> logger;
      private readonly IStreamingService streamingService;

      public CameraController(ILogger<CameraController> logger, IStreamingService streamingService)
      {
         this.logger = logger;
         this.streamingService = streamingService;
      }

      [HttpGet("isenabled")]
      public ActionResult<bool> IsEnabled()
      {
         return streamingService.IsEnabled;
      }

      [HttpGet("image")]
      public async Task<IActionResult> GetImageCapture()
      {
         var pictureBytes = await  streamingService.CaptureImageAsync();
         return File(pictureBytes, "image/jpeg");
      }
   }
}