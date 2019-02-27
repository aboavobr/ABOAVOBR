using System;
using System.Collections.Generic;
using aboavobr.raspberrypi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aboavobr.raspberrypi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class DebugController : ControllerBase
   {
      private readonly ILogger<DebugController> logger;
      private readonly ISerialCommunicationService serialCommunicationService;

      public DebugController(ISerialCommunicationService serialCommunicationService, ILogger<DebugController> logger)
      {
         this.serialCommunicationService = serialCommunicationService;
         this.logger = logger;
      }

      // GET api/debug
      [HttpGet]
      public ActionResult<string> Get()
      {
         return "It's alive";
      }

      // GET api/debug/serial/ports
      [HttpGet("serial/ports")]
      public ActionResult<IEnumerable<string>> GetSerialPorts()
      {
         return new ActionResult<IEnumerable<string>>(serialCommunicationService.GetAvailableSerialPorts());
      }

      [HttpGet("serial/port")]
      public ActionResult<string> GetSerialPort()
      {
         return new ActionResult<string>(serialCommunicationService.PortName);
      }

      [HttpPost("serial")]
      public ActionResult WriteSerial(string command)
      {
         serialCommunicationService.SendMessage(command);
         
         return Ok();
      }
   }
}