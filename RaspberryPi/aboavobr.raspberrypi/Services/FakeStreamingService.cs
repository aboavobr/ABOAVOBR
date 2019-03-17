using System.Threading.Tasks;

namespace aboavobr.raspberrypi.Services
{
   public class FakeStreamingService : IStreamingService
   {
      public bool IsEnabled => false;

      public Task<byte[]> CaptureImageAsync()
      {
         throw new System.NotImplementedException();
      }

      public void CaptureVideoStream()
      {
         throw new System.NotImplementedException();
      }
   }
}
