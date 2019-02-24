namespace aboavobr.raspberrypi.Services
{
   public interface ISerialPortFactory
   {
      ISerialPort CreatePort(string portName);

      ISerialPort CreateFakePort();
   }
}
