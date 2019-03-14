using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
      private readonly ISerialPortService serialPortService;
      private ISerialPort serialPort;

      public SerialCommunicationService(
         IHostingEnvironment environment,
         IConfiguration configuration,
         ILogger<SerialCommunicationService> logger,
         ISerialPortFactory serialPortFactory,
         ISerialPortService serialPortService)
      {
         this.environment = environment;
         this.configuration = configuration;
         this.logger = logger;
         this.serialPortFactory = serialPortFactory;
         this.serialPortService = serialPortService;
         InitializeCommunicationPort();
      }

      public bool IsConnected => serialPort != null && serialPort.IsConnected;

      public string PortName => serialPort.Name;

      public void SendMessage(string message)
      {
         serialPort.Write(message);
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

               var availableDevices = serialPortService.GetAvailableSerialPorts().ToList();

               if (availableDevices.Count == 1)
               {
                  port = availableDevices[0];
                  logger.LogDebug($"Found exactly one available port - will use this one");
               }
               else if (availableDevices.Count > 1)
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
   }
}
