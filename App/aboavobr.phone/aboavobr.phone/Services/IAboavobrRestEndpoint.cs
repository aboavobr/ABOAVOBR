using System.Threading.Tasks;

namespace aboavobr.phone.Services
{
   public interface IAboavobrRestEndpoint
   {
      Task<bool> Connect(string url);

      Task SendCommandAsync(string valueToSend);

      Task<int> GetBatteryLifeAsync();

      Task<byte[]> GetImageAsync();
   }
}