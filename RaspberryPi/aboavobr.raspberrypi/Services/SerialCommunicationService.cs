using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace aboavobr.raspberrypi.Services
{
   public class SerialCommunicationService : ISerialCommunicationService
   {
      public IEnumerable<string> GetSerialPorts()
      {
         return GetAvailablePorts();
      }

      public static string[] GetAvailablePorts()
      {
         // Copied from https://github.com/JTrotta/MonoSerialPort/blob/006ac36503ef2c1e2a682961fc0720f358e21ca0/Port/SerialPort.cs
         var platform = (int)Environment.OSVersion.Platform;
         var availableSerialPorts = new List<string>();

         // Are we on Unix?
         if (platform == 4 || platform == 128)
         {
            var ttys = Directory.GetFiles("/dev/", "tty*");
            foreach (var devices in ttys)
            {
               if (devices.StartsWith("/dev/ttyS") || devices.StartsWith("/dev/ttyUSB") || devices.StartsWith("/dev/ttyACM"))
               {
                  availableSerialPorts.Add(devices);
               }
            }
         }
         else
         {
            // Do Windows Magic
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
                        availableSerialPorts.Add(port);
                     }
                  }
               }
            }
         }
         return availableSerialPorts.ToArray();
      }
   }
}
