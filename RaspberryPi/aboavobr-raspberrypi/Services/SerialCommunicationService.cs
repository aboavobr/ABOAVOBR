using System.IO.Ports;

namespace aboavobr.raspberrypi.Services
{
   public class SerialCommunicationService : ISerialCommunicationService
   {
      private readonly static SerialPort serialPort;

      static SerialCommunicationService()
      {
         // Make this configurable.
         serialPort = new SerialPort
         {
            PortName = "COM4", //Set your board COM
            BaudRate = 9600
         };
      }

      public bool IsOpen => serialPort.IsOpen;

      public void Open()
      {
         serialPort.Open();
      }
   }
}
