
namespace aboavobr.raspberrypi.Services
{
   public interface ISerialCommunicationService
   {
      bool IsOpen { get; }

      void Open();
   }
}
