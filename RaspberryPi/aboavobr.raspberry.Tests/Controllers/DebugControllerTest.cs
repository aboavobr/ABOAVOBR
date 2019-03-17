using aboavobr.raspberrypi.Controllers;
using aboavobr.raspberrypi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Tests.Controllers
{
   public class DebugControllerTest
   {
      private Mock<ILogger<DebugController>> loggerMock;
      private Mock<ISerialCommunicationService> serialCommunicationServiceMock;
      private Mock<ISerialPortService> serialPortServiceMock;

      [SetUp]
      public void Setup()
      {
         loggerMock = new Mock<ILogger<DebugController>>();
         serialCommunicationServiceMock = new Mock<ISerialCommunicationService>();
         serialPortServiceMock = new Mock<ISerialPortService>();
      }

      [Test]
      public void Get_ReturnsIsAlive()
      {
         var subject = CreateSubject();

         var actualValue = subject.Get().Value;

         Assert.AreEqual("It's alive", actualValue);
      }

      [Test]
      public void GetSerialPorts_ReturnsAllAvailablePorts()
      {
         var expectedPorts = new[]
         {
            "/dev/tty1",
            "/dev/ttyACM0",
         };

         serialPortServiceMock.Setup(x => x.GetAvailableSerialPorts()).Returns(expectedPorts);

         var subject = CreateSubject();

         var ports = subject.GetSerialPorts().Value;

         CollectionAssert.AreEqual(expectedPorts, ports);
      }

      [Test]
      public void GetSerialPort_ReturnsUsedPort()
      {
         const string ExpectedPort = "COM5";
         serialCommunicationServiceMock.SetupGet(x => x.PortName).Returns(ExpectedPort);

         var subject = CreateSubject();

         var port = subject.GetSerialPort().Value;

         Assert.AreEqual(ExpectedPort, port);
      }

      [Test]
      public void WriteSerial_GivenCommand_WritsToCommunicationService()
      {
         var subject = CreateSubject();

         var result = subject.WriteSerial("command");

         Assert.IsInstanceOf(typeof(OkResult), result);
         serialCommunicationServiceMock.Verify(x => x.SendMessage("command"));
      }

      private DebugController CreateSubject()
      {
         return new DebugController(serialCommunicationServiceMock.Object, loggerMock.Object, serialPortServiceMock.Object);
      }
   }
}