using System;

namespace aboavobr.raspberrypi.Services
{
   public interface ISerialPort
   {
      event EventHandler<string> MessageReceivedEvent;

      event EventHandler<bool> ConnectionStatusChangedEvent;

      bool IsConnected { get; }

      bool Connect();

      void Disconnect();

      bool Write(string message);
   }
}
