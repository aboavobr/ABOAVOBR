using System.Collections.Generic;

namespace aboavobr.raspberrypi.Services
{
   public class SerialCommunicationService : ISerialCommunicationService
   {
      public IEnumerable<string> GetSerialPorts()
      {
         return new[] { "Not yet implemented" };
      }
   }
}
