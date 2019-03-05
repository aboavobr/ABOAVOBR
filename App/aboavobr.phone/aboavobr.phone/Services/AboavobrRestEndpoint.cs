using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace aboavobr.phone.Services
{
   public class AboavobrRestEndpoint : IAboavobrRestEndpoint
   {
      private const string AppApiEndpoint = "/api/app";
      private const string DebugApiEndpoint = "/api/debug";

      private string baseUrl;

      private readonly HttpClient client;

      public AboavobrRestEndpoint()
      {
         client = new HttpClient();
         client.Timeout = TimeSpan.FromSeconds(10);
         client.MaxResponseContentBufferSize = 256000;
      }

      public async Task<bool> Connect(string url)
      {
         var heartbeatUrl = $"{url}{AppApiEndpoint}/heartbeat";

         baseUrl = string.Empty;
         var uri = new Uri(heartbeatUrl);

         var response = await client.GetAsync(uri);

         if (response.IsSuccessStatusCode)
         {
            baseUrl = url;
            return true;
         }

         return false;
      }

      public async Task SendCommandAsync(string valueToSend)
      {
         var commandUrl = $"{baseUrl}{DebugApiEndpoint}/serial";

         var uri = new Uri(commandUrl);
         var response = await client.PostAsync(uri, new StringContent(valueToSend, Encoding.UTF8, "application/json"));
      }
   }
}
