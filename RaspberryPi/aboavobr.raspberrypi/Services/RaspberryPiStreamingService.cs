using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

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

         Pi.Init<BootstrapWiringPi>();
      }

      public bool IsEnabled { get; }

      public string CaptureImage()
      {
         var pictureBytes = Pi.Camera.CaptureImageJpeg(640, 480);
         var targetPath = "/home/capture.jpg";
         if (File.Exists(targetPath))
         {
            File.Delete(targetPath);
         }

         File.WriteAllBytes(targetPath, pictureBytes);
         logger.LogDebug($"Took picture -- Byte count: {pictureBytes.Length}");

         return targetPath;
      }

      public void CaptureVideoStream()
      {
         throw new System.NotImplementedException();
      }
   }
}
