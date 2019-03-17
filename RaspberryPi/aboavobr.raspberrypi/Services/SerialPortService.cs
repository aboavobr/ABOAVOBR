using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace aboavobr.raspberrypi.Services
{
   public class SerialPortService : ISerialPortService
   {
      private readonly ILogger<SerialPortService> logger;
      private readonly string[] availablePorts;

      public SerialPortService(ILogger<SerialPortService> logger)
      {
         this.logger = logger;
         availablePorts = GetAvailableCommunicationPorts();
      }

      public IEnumerable<string> GetAvailableSerialPorts()
      {
         return availablePorts;
      }

      private string[] GetAvailableCommunicationPorts()
      {
         // Copied from https://github.com/JTrotta/MonoSerialPort/blob/006ac36503ef2c1e2a682961fc0720f358e21ca0/Port/SerialPort.cs
         
         var availableSerialPorts = new List<string>();

         logger.LogDebug("Starting to scan the available communication ports...");

         // Are we on Unix?
         if (IsUnix())
         {
            logger.LogDebug("We are on a Unix System, will look for /dev/tty* ports");
            availableSerialPorts.AddRange(GetUnixTTYs());
         }
         else
         {
            logger.LogDebug("We are on a Windows System, will check registriy for COM Ports");

            // Do Windows Magic
            availableSerialPorts.AddRange(GetWindowsComPorts());
         }

         return availableSerialPorts.ToArray();
      }

      private bool IsUnix()
      {
         var platform = (int)Environment.OSVersion.Platform;
         return platform == 4 || platform == 128;
      }

      private IEnumerable<string> GetUnixTTYs()
      {
         var availableSerialPorts = new List<string>();
         var availableDevices = Directory.GetFiles("/dev/", "tty*");
         foreach (var device in availableDevices)
         {
            logger.LogDebug($"Found Port: {device}");
            if (device.StartsWith("/dev/ttyS") || device.StartsWith("/dev/ttyUSB") || device.StartsWith("/dev/ttyACM"))
            {
               availableSerialPorts.Add(device);
            }
         }

         return availableSerialPorts;
      }

      private IEnumerable<string> GetWindowsComPorts()
      {
         var availableComPorts = new List<string>();
         using (var subkey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
         {
            if (subkey != null)
            {
               var names = subkey.GetValueNames();
               foreach (var value in names)
               {
                  var port = subkey.GetValue(value, "").ToString();
                  if (!string.IsNullOrEmpty(port))
                  {
                     logger.LogDebug($"Found Port: {port}");

                     availableComPorts.Add(port);
                  }
               }
            }
         }

         return availableComPorts;
      }
   }
}
