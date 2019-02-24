using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace aboavobr.raspberrypi.Services
{
   public class SerialPortFactory : ISerialPortFactory
   {
      private readonly IConfiguration configuration;
      private readonly ILogger<SerialPortFactory> logger;

      public SerialPortFactory(IConfiguration configuration, ILogger<SerialPortFactory> logger)
      {
         this.configuration = configuration;
         this.logger = logger;
      }

      public ISerialPort CreateFakePort()
      {
         return new FakeSerialPort(configuration, logger);
      }

      public ISerialPort CreatePort(string portName)
      {
         return new SerialPort(portName, true);
      }
   }
}
