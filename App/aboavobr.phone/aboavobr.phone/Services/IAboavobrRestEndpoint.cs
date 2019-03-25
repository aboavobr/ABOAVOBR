using System.Threading.Tasks;
using aboavobr.phone.ViewModels;

namespace aboavobr.phone.Services
{
   public interface IAboavobrRestEndpoint
   {
      Task<bool> Connect(string url);

      Task SendCommandAsync(string valueToSend);

      Task<int> GetBatteryLifeAsync();

      Task<byte[]> GetImageAsync();
      Task<bool> IsCameraSupported();

      Task<bool> SendMoveCommandAsync(Direction direction);
   }
}