﻿using System.Threading.Tasks;

namespace aboavobr.phone.Services
{
   public interface IAboavobrRestEndpoint
   {
      Task<bool> Connect(string url);
   }
}