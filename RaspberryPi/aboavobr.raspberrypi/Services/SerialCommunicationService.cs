using System;
using System.Threading;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace aboavobr.raspberrypi.Services
{
   public class SerialCommunicationService : ISerialCommunicationService
   {
      private const string BatteryLifeIdentifier = "batteryLife";
      private const string GetBatteryLifeCommand = "getBatteryLife";
      private const string SendMoveCommand = "mv:{0}";
      private const string CommandSeparator = ":";

      private readonly IHostingEnvironment environment;
      private readonly IConfiguration configuration;
      private readonly ILogger<SerialCommunicationService> logger;
      private readonly ISerialPortFactory serialPortFactory;

      private int batteryLife = -1;

      private readonly ISerialPortService serialPortService;
      private ISerialPort serialPort;
      private Timer batteryLifeTimer;

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
         SetupBatteryLifePolling();
      }

      public bool IsConnected => serialPort != null && serialPort.IsConnected;

      public string PortName => serialPort.Name;

      public void SendMessage(string message)
      {
         serialPort.Write(message);
      }

      public int GetBatteryLife()
      {
         return batteryLife;
      }

      public void Move(Direction direction)
      {
         logger.LogDebug($"Sending command to move in direction {direction}");

         var message = string.Format(SendMoveCommand, (int)direction);
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

      private void SetupBatteryLifePolling()
      {
         void SendBatteryLifeRequest(object state)
         {
            serialPort.Write(GetBatteryLifeCommand);

            logger.LogDebug($"Sending get battery life request");
         }

         batteryLifeTimer = new Timer(SendBatteryLifeRequest, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(30));
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

         if (message.StartsWith($"{BatteryLifeIdentifier}{CommandSeparator}"))
         {
            var batteryLifeString = message.Split(CommandSeparator)[1];

            if (int.TryParse(batteryLifeString, out var currentState))
            {
               batteryLife = currentState;
               logger.LogDebug($"Read Battery Life: {batteryLife}");
            }
            else
            {
               logger.LogDebug($"Could not read battery life: {batteryLifeString}");
            }
         }
      }

      private void OnSerialPortConnectionChanged(object sender, bool isConnected)
      {
         logger.LogDebug($"Serial Port Connection Changed: {isConnected}");
      }
   }
}
