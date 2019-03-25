using aboavobr.raspberrypi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace aboavobr.raspberry.Tests.Services
{
   public class SerialCommunicationServiceTest
   {
      private Mock<IHostingEnvironment> hostingEnvironmentMock;
      private Mock<IConfiguration> configurationMock;
      private Mock<ILogger<SerialCommunicationService>> loggerMock;
      private Mock<ISerialPortFactory> serialPortFactoryMock;
      private Mock<ISerialPortService> serialPortServiceMock;

      [SetUp]
      public void Setup()
      {
         hostingEnvironmentMock = new Mock<IHostingEnvironment>();
         configurationMock = new Mock<IConfiguration>();
         loggerMock = new Mock<ILogger<SerialCommunicationService>>();
         serialPortFactoryMock = new Mock<ISerialPortFactory>();
         serialPortServiceMock = new Mock<ISerialPortService>();
      }

      private SerialCommunicationService CreateSubject()
      {
         return new SerialCommunicationService(
            hostingEnvironmentMock.Object,
            configurationMock.Object,
            loggerMock.Object,
            serialPortFactoryMock.Object,
            serialPortServiceMock.Object);
      }
   }
}
