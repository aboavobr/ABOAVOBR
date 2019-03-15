using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static Unosquare.RaspberryIO.Pi;

namespace aboavobr.raspberrypi.Services
{
   /// <summary>
   /// This service uses the unosquare raspberry pi package. For more info see here: https://github.com/unosquare/raspberryio#the-camera-module
   /// 
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

      public async Task<string> CaptureImage()
      {
         var targetPath = $"image-{DateTime.UtcNow.Ticks}.jpg";
         if (File.Exists(targetPath))
         {
            File.Delete(targetPath);
         }

         var result = await Camera.CaptureImageJpegAsync(640, 480);
         await File.WriteAllBytesAsync(targetPath, result);

         logger.LogDebug($"Took picture: {targetPath}");

         return targetPath;
      }

      public void CaptureVideoStream()
      {
         throw new System.NotImplementedException();
      }
   }
}
