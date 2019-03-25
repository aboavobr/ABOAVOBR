using System.Collections.Generic;

namespace aboavobr.raspberrypi.Services
{
   public interface ISerialPortService
   {
      IEnumerable<string> GetAvailableSerialPorts();
   }
}
