using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace aboavobr.raspberrypi.Services
{
   public class SerialCommunicationService : ISerialCommunicationService
   {
      private readonly IHostingEnvironment environment;
      private readonly IConfiguration configuration;
      private readonly ILogger<SerialCommunicationService> logger;
      private readonly ISerialPortFactory serialPortFactory;
      private ISerialPort serialPort;

      public SerialCommunicationService(
         IHostingEnvironment environment,
         IConfiguration configuration,
         ILogger<SerialCommunicationService> logger,
         ISerialPortFactory serialPortFactory)
      {
         this.environment = environment;
         this.configuration = configuration;
         this.logger = logger;
         this.serialPortFactory = serialPortFactory;

         InitializeCommunicationPort();
      }

      public bool IsConnected => serialPort != null && serialPort.IsConnected;

      public string PortName => serialPort.Name;

      public void SendMessage(string message)
      {
         serialPort.Write(message);
      }

      public IEnumerable<string> GetAvailableSerialPorts()
      {
         return GetAvailablePorts();
      }

      private void InitializeCommunicationPort()
      {
         var port = string.Empty;
         var useFakePort = false;

         if (environment.IsDevelopment())
         {
            logger.LogDebug("Is Debug Environment, will check for FakePort setting");

            useFakePort = configuration.GetValue<bool>("FakePort:IsActive");

            if (useFakePort)
            {
               logger.LogDebug("Using Fake Communication Port");
               port = "FakePort";
            }
            else
            {
               logger.LogDebug("Will not use fake port");
            }
         }

         if (!useFakePort)
         {
            var fixedPort = configuration.GetValue<string>("SerialPort");
            if (!string.IsNullOrEmpty(fixedPort))
            {
               logger.LogDebug($"Fixed Serial Port set in configuration: {fixedPort}");
               port = fixedPort;
            }
            else
            {
               logger.LogDebug("No fixed port configuration found - scanning for ports...");

               var availableDevices = GetAvailablePorts();

               if (availableDevices.Length == 1)
               {
                  port = availableDevices[0];
                  logger.LogDebug($"Found exactly one available port - will use this one");
               }
               else if (availableDevices.Length > 1)
               {
                  port = availableDevices[0];
                  logger.LogDebug($"Found multiple ports - will use first one. If this is not the right device, consider specifying it using the SerialPort configuration");
               }
               else
               {
                  logger.LogDebug("No Ports found. Make sure device is connected properly and if run in a docker container --device flag is used. Otherwise see docs.");
                  throw new InvalidOperationException("No Ports found. Make sure device is connected properly and if run in a docker container --device flag is used. Otherwise see docs.");
               }
            }
         }

         ConnectSerialPort(port, useFakePort);
      }

      private void ConnectSerialPort(string port, bool useFakePort)
      {
         logger.LogDebug($"Establishing Connection to Port {port}...");

         if (useFakePort)
         {
            serialPort = serialPortFactory.CreateFakePort();
         }
         else
         {
            serialPort = serialPortFactory.CreatePort(port);
         }

         serialPort.ConnectionStatusChangedEvent += OnSerialPortConnectionChanged;
         serialPort.MessageReceivedEvent += OnSerialPortMessageReceived;
         serialPort.Connect();
      }

      private void OnSerialPortMessageReceived(object sender, string message)
      {
         logger.LogDebug($"Received Message from Serial Port: {message}");

         /* Buffer Messages? */
      }

      private void OnSerialPortConnectionChanged(object sender, bool isConnected)
      {
         logger.LogDebug($"Serial Port Connection Changed: {isConnected}");
      }

      private string[] GetAvailablePorts()
      {
         // Copied from https://github.com/JTrotta/MonoSerialPort/blob/006ac36503ef2c1e2a682961fc0720f358e21ca0/Port/SerialPort.cs
         var platform = (int)Environment.OSVersion.Platform;
         var availableSerialPorts = new List<string>();

         logger.LogDebug("Starting to scan the available communication ports...");

         // Are we on Unix?
         if (platform == 4 || platform == 128)
         {
            logger.LogDebug("We are on a Unix System, will look for /dev/tty* ports");

            var ttys = Directory.GetFiles("/dev/", "tty*");
            foreach (var device in ttys)
            {
               logger.LogDebug($"Found Port: {device}");

               if (device.StartsWith("/dev/ttyS") || device.StartsWith("/dev/ttyUSB") || device.StartsWith("/dev/ttyACM"))
               {
                  availableSerialPorts.Add(device);
               }
            }
         }
         else
         {
            logger.LogDebug("We are on a Windows System, will check registriy for COM Ports");

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
                        logger.LogDebug($"Found Port: {port}");

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
