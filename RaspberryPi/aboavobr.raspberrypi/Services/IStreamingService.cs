﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aboavobr.raspberrypi.Services
{
   public interface IStreamingService
   {
      bool IsEnabled { get; }

      Task<byte[]> CaptureImageAsync();

      void CaptureVideoStream();
   }
}
