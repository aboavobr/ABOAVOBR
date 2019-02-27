using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace aboavobr.raspberrypi.Services
{
   public class FakeSerialPort : ISerialPort
   {
      private readonly string inputFile;
      private readonly string outputFile;
      private readonly ILogger logger;
      private FileSystemWatcher inputFileSystemWatcher;
      private FileSystemWatcher outputFileSystemWatcher;
      private bool inputEventRaised;

      public FakeSerialPort(IConfiguration confguration, ILogger logger)
      {
         this.logger = logger;

         inputFile = confguration.GetValue<string>("FakePort:SerialInputFile");
         outputFile = confguration.GetValue<string>("FakePort:SerialOutputFile");

         logger.LogDebug($"Using Input File: {inputFile}");
         logger.LogDebug($"Using Output File: {outputFile}");

         if (File.Exists(inputFile))
         {
            File.Delete(inputFile);
         }

         if (File.Exists(outputFile))
         {
            File.Delete(outputFile);
         }
      }

      public event EventHandler<string> MessageReceivedEvent;
      public event EventHandler<bool> ConnectionStatusChangedEvent;

      public bool IsConnected => File.Exists(inputFile) && File.Exists(outputFile);

      public string Name => "Fake Port";

      public bool Connect()
      {
         if (!File.Exists(inputFile))
         {
            File.AppendAllLines(inputFile, Enumerable.Empty<string>());
         }

         if (!File.Exists(outputFile))
         {
            File.AppendAllLines(outputFile, Enumerable.Empty<string>());
         }

         var inputFileInfo = new FileInfo(inputFile);
         var outputFileInfo = new FileInfo(outputFile);

         inputFileSystemWatcher = new FileSystemWatcher(inputFileInfo.DirectoryName);
         //inputFileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
         inputFileSystemWatcher.Filter = inputFileInfo.Name;

         outputFileSystemWatcher = new FileSystemWatcher(outputFileInfo.DirectoryName);
         outputFileSystemWatcher.Filter = outputFileInfo.Name;

         inputFileSystemWatcher.Deleted += OnFileDeleted;
         inputFileSystemWatcher.Changed += OnInputFileChanged;
         outputFileSystemWatcher.Deleted += OnFileDeleted;

         inputFileSystemWatcher.EnableRaisingEvents = true;
         outputFileSystemWatcher.EnableRaisingEvents = true;

         return true;
      }

      public void Disconnect()
      {
         inputFileSystemWatcher.Deleted -= OnFileDeleted;
         inputFileSystemWatcher.Changed -= OnInputFileChanged;
         outputFileSystemWatcher.Deleted -= OnFileDeleted;

         inputFileSystemWatcher.EnableRaisingEvents = false;
         outputFileSystemWatcher.EnableRaisingEvents = false;

         if (File.Exists(inputFile))
         {
            File.Delete(inputFile);
         }

         if (File.Exists(outputFile))
         {
            File.Delete(outputFile);
         }
      }

      public bool Write(string message)
      {
         logger.LogDebug($"Message to send: {message}");

         File.AppendAllLines(outputFile, new[] { message });
         return true;
      }

      private void OnFileDeleted(object sender, FileSystemEventArgs fileSystemEventArgs)
      {
         ConnectionStatusChangedEvent?.Invoke(this, IsConnected);
      }

      private void OnInputFileChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
      {
         if (inputEventRaised)
         {
            // We get 2 events for every save of the file...
            inputEventRaised = false;
            return;
         }

         inputEventRaised = true;

         var inputText = File.ReadAllText(inputFile);

         logger.LogDebug($"Input Received: {inputText}");

         MessageReceivedEvent?.Invoke(this, inputText);
      }
   }
}
