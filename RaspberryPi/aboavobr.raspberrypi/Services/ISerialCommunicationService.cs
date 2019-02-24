
using System.Collections.Generic;

namespace aboavobr.raspberrypi.Services
{
   public interface ISerialCommunicationService
   {
      bool IsConnected { get; }

      void SendMessage(string message);

      IEnumerable<string> GetAvailableSerialPorts();
   }
}
