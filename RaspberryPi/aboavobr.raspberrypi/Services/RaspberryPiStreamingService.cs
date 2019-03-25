using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;

namespace aboavobr.raspberrypi.Services
{
   /// <summary>
   /// This service uses the unosquare raspberry pi package. For more info see here: https://github.com/unosquare/raspberryio#the-camera-module
   /// This relies on the raspistill and raspivid, if they are not available this won't work. 
   /// </summary>
   public class RaspberryPiStreamingService : IStreamingService
   {
      private const string RaspberryPiCameraDevice = "/dev/vchiq";

      private readonly ILogger<RaspberryPiStreamingService> logger;

      public RaspberryPiStreamingService(ILogger<RaspberryPiStreamingService> logger)
      {
         this.logger = logger;

         IsEnabled = Directory.GetFiles("/dev/", "*").Any(d => d == RaspberryPiCameraDevice);

         if (IsEnabled)
         {
            logger.LogDebug("Camera was detected, Streaming Service is enabled.");
         }
         else
         {
            logger.LogDebug("Camera device \"/dev/vchiq\" was not detected, Streaming service is disabled");
         }
      }

      public bool IsEnabled { get; }

      public async Task<byte[]> CaptureImageAsync()
      {
         var pictureBytes = await Pi.Camera.CaptureImageJpegAsync(640, 480);

         logger.LogDebug($"Took picture -- Byte count: {pictureBytes.Length}");

         return pictureBytes;
      }

      public void CaptureVideoStream()
      {
         throw new System.NotImplementedException();
      }
   }
}
