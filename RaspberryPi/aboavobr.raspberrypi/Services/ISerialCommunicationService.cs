
using System.Collections.Generic;

namespace aboavobr.raspberrypi.Services
{
   public interface ISerialCommunicationService
   {
      IEnumerable<string> GetSerialPorts();
   }
}
