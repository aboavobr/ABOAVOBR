using Microsoft.AspNetCore.Mvc;

namespace aboavobr.raspberrypi.Services
{
   public interface IStreamingService
   {
      bool IsEnabled { get; }

      string CaptureImage();

      void CaptureVideoStream();
   }
}
