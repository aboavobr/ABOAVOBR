using System.Threading.Tasks;
using aboavobr.raspberrypi.Controllers;
using aboavobr.raspberrypi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Tests.Controllers
{
   public class DebugControllerTest
   {
      private Mock<ILogger<DebugController>> loggerMock;
      private Mock<ISerialCommunicationService> serialCommunicationServiceMock;

      [SetUp]
      public void Setup()
      {
         loggerMock = new Mock<ILogger<DebugController>>();
         serialCommunicationServiceMock = new Mock<ISerialCommunicationService>();
      }

      [Test]
      public void Get_ReturnsIsAlive()
      {
         var subject = CreateSubject();

         var actualValue = subject.Get().Value;

         Assert.AreEqual("It's alive", actualValue);
      }

      private DebugController CreateSubject()
      {
         return new DebugController(serialCommunicationServiceMock.Object, loggerMock.Object);
      }
   }
}