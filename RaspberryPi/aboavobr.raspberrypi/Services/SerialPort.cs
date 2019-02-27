using System;
using System.Text;
using MonoSerialPort;

namespace aboavobr.raspberrypi.Services
{
   public class SerialPort : ISerialPort
   {
      private readonly SerialPortInput serialPortInput;

      public SerialPort(string portName, bool isVirtual)
      {
         serialPortInput = new SerialPortInput(portName, isVirtual);
         serialPortInput.SetPort(portName, 9600);

         serialPortInput.ConnectionStatusChanged += OnConnectionStatusChanged;
         serialPortInput.MessageReceived += OnMessageReceived;
      }

      public event EventHandler<string> MessageReceivedEvent;

      public event EventHandler<bool> ConnectionStatusChangedEvent;

      public bool IsConnected => serialPortInput.IsConnected;

      public bool Connect()
      {
         return serialPortInput.Connect();
      }

      public void Disconnect()
      {
         serialPortInput.Disconnect();
      }

      public bool Write(string message)
      {
         var packet = Encoding.ASCII.GetBytes(message);
         return serialPortInput.SendMessage(packet);
      }

      private void OnMessageReceived(object sender, MessageReceivedEventArgs args)
      {
         var message = Encoding.ASCII.GetString(args.Data);
         MessageReceivedEvent?.Invoke(this, message);
      }

      private void OnConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
      {
         ConnectionStatusChangedEvent?.Invoke(this, args.Connected);
      }
   }
}
